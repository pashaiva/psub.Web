﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Entities;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class PublicationCommentService : IPublicationCommentService
    {
        private readonly IRepository<PublicationComment> _publicationCommentRepository;
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;

        public PublicationCommentService(IRepository<PublicationComment> publicationCommentRepository,
            IUserService userService,
            ILikeService likeService)
        {
            _publicationCommentRepository = publicationCommentRepository;
            _userService = userService;
            _likeService = likeService;
        }

        public PublicationCommentListItem Save(int objectId, string text, int commentId)
        {
            var currentUser = _userService.GetCurrentUser();

            var saveComment = new PublicationComment
            {
                Publication = new Publication { Id = objectId },
                Text = text,
                UserGuid = currentUser.UserGuid,
                UserName = currentUser.Name,
                AnswerTo = commentId > 0 ? new PublicationComment { Id = commentId } : null,
                Created = DateTime.Now,
                Guid = Guid.NewGuid().ToString()
            };

            _publicationCommentRepository.SaveOrUpdate(saveComment);

            if (saveComment.AnswerTo != null)
            {
                //var comment = _publicationCommentRepository.Get(saveComment.AnswerTo.Id);
                //_emailService.SendEmail(_userService.GetUserByGuid(comment.UserGuid).Email,
                //                        "На ваш комментарий ответили",
                //                        _emailProvider.GetCreateEditBody(new PublicationTask
                //                        {
                //                            Publication = comment.Publication,
                //                            ToUserName = comment.UserName,
                //                            FromUserName = currentUser.FIO,
                //                            Created = comment.Created,
                //                            CreatorName = comment.Guid
                //                        }));
            }

            var user = _userService.GetUserByGuid(saveComment.UserGuid);

            return new PublicationCommentListItem
            {
                Id = saveComment.Id.ToString(CultureInfo.InvariantCulture),
                Comment = saveComment.Text,
                Author = saveComment.UserName,
                Date = saveComment.Created.ToString("dd.MM.yyyy, H:mm:ss"),
                CanReply = "true",
                ParentId = saveComment.AnswerTo != null ? saveComment.AnswerTo.Id.ToString() : null,
                UserAvatar = string.Format("http://www.gravatar.com/avatar/{0}?d={1}", HashEmailForGravatar(user != null ? user.Email : string.Empty), "http://media.tumblr.com/tumblr_lak5phfeXz1qzqijq.png"),
                Guid = saveComment.Guid,
                DisLikeUsers = string.Empty,
                LikeUsers = string.Empty
            };
        }

        public ListPublicationComment GetPublicationCommentsByPublicationId(int id, int pageSize, int pageNumber)
        {
            var url = HttpContext.Current.Request.Url;
            var commentList = _publicationCommentRepository.Query().Where(m => m.Publication.Id == id).OrderBy(m => m.Id);
            var result = new ListPublicationComment
            {
                Items = commentList.Any()
                            ? commentList.ToList().Select(m => new PublicationCommentListItem//.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            {
                                Id = m.Id.ToString(CultureInfo.InvariantCulture),
                                Comment = m.Text,
                                Author = m.UserName,
                                Date = m.Created.ToString("dd.MM.yyyy, H:mm:ss"),
                                CanReply = "true",
                                ParentId = m.AnswerTo != null ? m.AnswerTo.Id.ToString() : null,
                                UserAvatar = string.Format("http://www.gravatar.com/avatar/{0}?d={1}", HashEmailForGravatar(_userService.GetUserByGuid(m.UserGuid) != null ? _userService.GetUserByGuid(m.UserGuid).Email : string.Empty), "http://media.tumblr.com/tumblr_lak5phfeXz1qzqijq.png"),
                                Guid = m.Guid,
                                LikeCount = m.Likes != null && m.Likes.Any() ? m.Likes.Count(like => like.IsLike) : 0,
                                DisLikeCount = m.Likes != null && m.Likes.Any() ? m.Likes.Count(like => !like.IsLike) : 0,
                                DisLikeUsers = m.Likes != null && m.Likes.Any() ? m.Likes.Where(like => !like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty,
                                LikeUsers = m.Likes != null && m.Likes.Any() ? m.Likes.Where(like => like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty
                            }).ToList()
                            : new List<PublicationCommentListItem>(),
                Count = commentList.Count()
            };

            return result;
        }

        public static string HashEmailForGravatar(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  D:\VS\GitHub\psub.Web\psub.DataService\Abstract\
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}
