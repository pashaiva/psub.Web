using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class DrawingService : IDrawingService
    {
        private Bitmap _bmp;
        private Graphics _graph;
        private readonly Pen _penGreen = new Pen(Color.DarkGreen, 1);
        private readonly Pen _penBlue = new Pen(Color.Blue, 2);
        private Font _font = new Font("Arial", 14);
        private readonly SolidBrush _backGroundWhiteBrush = new SolidBrush(Color.White);
        private readonly SolidBrush _blackBrush = new SolidBrush(Color.Black);
        private string filePath = string.Empty;

        private readonly IRepository<FullDataParameter> _fullDataParameterRepository;

        public DrawingService(IRepository<FullDataParameter> fullDataParameterRepository)
        {
            _fullDataParameterRepository = fullDataParameterRepository;
        }

        public string GetFile(int id, string name)
        {
            var yKoef = 10;
            var list = _fullDataParameterRepository.Query().Where(m => m.Name == name && m.ControlObject.Id == id).OrderBy(m => m.LastUpdate).ToList();
            foreach (var fullDataParameter in list)
            {
                try
                {
                    if (Convert.ToDouble(fullDataParameter.Value.Trim().Replace(".", ",")) < 0.001)
                    {
                        fullDataParameter.Value = "0.0";
                    }
                }
                catch (Exception)
                {
                    fullDataParameter.Value = "0.0";
                }
            }
            var width = list.Count < 5000 ? list.Count : 5000;
            var ymin = list.Min(m => Convert.ToDouble(m.Value.Replace(".", ",")));
            var ymax = list.Max(m => Convert.ToDouble(m.Value.Replace(".", ",")));
            if ((ymax - ymin) < 10)
            {
                yKoef *= 7;
            }
            width = list.Count < 1000 ? width * 5 : width;
            width += 100;
            var heigth = (int)(ymax - ymin) * yKoef;
            _font = new Font("Arial", 2.5f * 0.6f * 10);

            _bmp = new Bitmap(width, heigth + 100);
            _graph = Graphics.FromImage(_bmp);

            _graph.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            _graph.FillRectangle(_backGroundWhiteBrush, 0, 0, width, heigth + yKoef * 2);


            for (var i = 5; i >= 0; i--)
            {
                _graph.DrawString((ymin + (ymax - ymin) * i / 5).ToString(CultureInfo.InvariantCulture), _font, _blackBrush, 5, (float)(heigth - (heigth / 5 * i)) + yKoef);
            }
            DateTime date = DateTime.Now;
            var x = 100;
            var y0 = 0f;
            var isStart = true;
            var shag = (int)Math.Round((float)((float)list.Count / (float)width) + 0.5, 0);
            var xShag = list.Count < 1000 ? width / list.Count : 1;
            for (var index = 0; index < list.Count; index += shag)
            {
                var fullDataParameter = list[index];
                if (Math.Abs(y0 - 0) < 0.001)
                {
                    y0 = (float)(Convert.ToDouble(fullDataParameter.Value.Replace(".", ",")) - ymin) * yKoef + yKoef;
                }
                else
                {
                    //_graph.DrawEllipse(_penGreen, x, (float)(Convert.ToDouble(fullDataParameter.Value.Replace(".", ",")) - ymin) * 100, 2, 2);
                    //_graph.DrawEllipse(_penGreen, x, (float)(Convert.ToDouble(fullDataParameter.Value.Replace(".", ",")) - ymin) * 100, 1, 1);
                    _graph.DrawLine(_penBlue, x - xShag, y0, x,
                                    (float)(heigth - (Convert.ToDouble(fullDataParameter.Value.Replace(".", ",")) - ymin) * yKoef) + yKoef);
                    y0 = (float)(heigth - (Convert.ToDouble(fullDataParameter.Value.Replace(".", ",")) - ymin) * yKoef) + yKoef;
                }
                if (date.Year != fullDataParameter.LastUpdate.Year || date.Month != fullDataParameter.LastUpdate.Month ||
                    date.Day != fullDataParameter.LastUpdate.Day || isStart)
                {
                    _graph.DrawString(fullDataParameter.LastUpdate.ToString(CultureInfo.InvariantCulture), _font, _blackBrush, isStart ? 1 : x, 10);
                }
                else
                    if (x % (100) == 0 && x > 170)
                    {
                        _graph.DrawString(fullDataParameter.LastUpdate.ToShortTimeString(), _font, _blackBrush, x, 10);
                        _graph.DrawLine(_penGreen, x, 10, x, 40);
                        _graph.DrawLine(_penGreen, x + 1, 10, x + 1, 40);
                    }

                isStart = false;
                x += xShag;
            }
            x = 0;
            while (x <= width)
            {
                for (int i = 0; i <= 5; i++)
                {
                    _graph.DrawLine(_penGreen, x * 2, (heigth - (heigth / 5 * i)) + yKoef, x * 2 + 5, (heigth - (heigth / 5 * i)) + yKoef);
                }
                x += shag;
            }

            var saveFolder = HttpContext.Current.Server.MapPath(string.Format("{0}", "TEMP"));
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            filePath = saveFolder + @"\" + Guid.NewGuid() + ".jpg";
            _bmp.Save(filePath);

            return filePath;
        }
    }
}
