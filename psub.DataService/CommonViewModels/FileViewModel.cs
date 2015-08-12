using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Psub.Shared;

namespace Psub.DataService.CommonViewModels
{
    public class FileViewModel
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string Path { get; set; }
    }

    public class FileEditViewModel
    {
        public List<FileViewModel> Files { get; set; }
        public bool UserEdit { get; set; }
        public bool UserRead { get; set; }

        public string Type { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ActionAddition { get; set; }

        public bool MakeReturnFile { get; set; }
        public string ReturnFileInfo { get; set; }
        public bool IsFileConfirm { get; set; }

        [Display(Name = LanguageConstants.EditDate)]
        public DateTime UploadDate { get; set; }

        public bool IsViewAllFileInFolder { get; set; }
        public bool IsViewConfirmReject { get; set; }
        public bool IsViewSend { get; set; }
    }

    enum FileStructureType
    {
        Site=1,
        Document=2,
        TypikalWork=3
    }
}
