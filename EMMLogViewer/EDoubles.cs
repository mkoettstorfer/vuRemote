using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMMLogViewer
{
    public class EDoubles
    {
        private string _Line1;
        private string _Line2;
        private Int32 vCount1 = 0;
        private Int32 vCount2 = 0;

        public string Line1 { get { return _Line1; } set { _Line1 = value; } }
        public string Line2 { get { return _Line2; } set { _Line2 = value; } }
        public Int32 Count1 { get { return getCount1(); } }
        public Int32 Count2 { get { return getCount2(); } }

        //Constructor
        public EDoubles(){
            Clear();
        }

        //Constructor
        public EDoubles(string aLine1, string aLine2)
        {
            Line1 = aLine1;
            Line2 = aLine2;
        }

        private Int32 getCount1()
        {
            return vCount1;
        }

        private Int32 getCount2()
        {
            return vCount2;
        }

        //Clear
        public void Clear()
        {
            Line1 = "";
            Line2 = "";
            vCount1 = 0;
            vCount2 = 0;
        }

        public Boolean Add(string aLine)
        {
            if (Line1 == "")
            {
                Line1 = aLine;
                vCount1 = 1;
                return true;
            }
            else if (Line2 == "")
            {
                string[] data = aLine.Split(new Char[] { ' ', '\t' });
                if(Line1.Contains(data[3])){
                    vCount1 += 1;
                }else{
                    Line2 = aLine;
                    vCount2 = 1;
                }
                return true;
            }
            else {
                string[] data = aLine.Split(new Char[] { ' ', '\t' });
                Boolean found = false;
                if (Line1.Contains(data[3])){ found = true; vCount1 += 1;}
                if (Line2.Contains(data[3])){ found = true; vCount2 += 1; }
                return found;
            }

        }

    }
        
}
