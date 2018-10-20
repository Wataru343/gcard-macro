using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace gcard_macro.WebDriber
{
    class HtmlAgilityPackWindow : IWindow
    {
        private Point Position_ = new Point();
        private Size Size_ = new Size();
        public Point Position { get => new Point(); set => Position_ = value; }
        public Size Size { get => new Size(); set => Size_ = value; }

        public void FullScreen()
        {
            throw new NotImplementedException();
        }

        public void Maximize() { }

        public void Minimize()
        {
            throw new NotImplementedException();
        }
    }
}
