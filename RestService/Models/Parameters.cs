using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class Parameters
    {
        private string date_string;
        private string freq;
        private DateTime date;
        private string cycle;
        private string showDate;
        private bool datewise = false;
        private DateTime dtdate;

        public string Date_string
        {
            get
            {
                return date_string;
            }

            set
            {
                date_string = value;
            }
        }

        public string Freq
        {
            get
            {
                return freq;
            }

            set
            {
                freq = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Cycle
        {
            get
            {
                return cycle;
            }

            set
            {
                cycle = value;
            }
        }

        public string ShowDate
        {
            get
            {
                return showDate;
            }

            set
            {
                showDate = value;
            }
        }

        public bool Datewise
        {
            get
            {
                return datewise;
            }

            set
            {
                datewise = value;
            }
        }

        public DateTime Dtdate
        {
            get
            {
                return dtdate;
            }

            set
            {
                dtdate = value;
            }
        }
    }
}