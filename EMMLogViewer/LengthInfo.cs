using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMMLogViewer
{
    public class LengthInfo
    {
        private string _Length;
        private string _Text;

        public string Length { get { return _Length; } set { _Length = value; } }
        public string Text { get { return _Text; } set { _Text = value; } }

        public LengthInfo(){
        }

        public LengthInfo(string aLength, string aText)
        {
            Length = aLength;
            Text = aText;
        }

    }

}
