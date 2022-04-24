using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCrontab;
using System.Collections;

namespace RestService.Models
{
    public class Frequency
    {

        public DateTime getPreviousOccurance(CrontabSchedule objCronTab, DateTime dtDate)
        {
            DateTime dtDate1 = objCronTab.GetNextOccurrence(DateTime.Now);
            DateTime dtDate2 = objCronTab.GetNextOccurrence(dtDate1);
            TimeSpan ts = dtDate2.Subtract(dtDate1);
            int days = ts.Days;
            days = days * 7;//For safeside
            ArrayList alDate = new ArrayList();
            foreach (DateTime dtNew in objCronTab.GetNextOccurrences(dtDate.AddDays(-days), dtDate.AddSeconds(1)))
            {
                alDate.Add(dtNew);
            }
            for (int i = alDate.Count - 1; i > 0; i--)
            {
                if (!((DateTime)alDate[i] > dtDate))
                {
                    return (DateTime)alDate[i];
                }
            }
            return dtDate;
        }
        public DateTime getPreviousOccurance2(CrontabSchedule objCronTab, DateTime dtDate)
        {
            DateTime dtDate1 = objCronTab.GetNextOccurrence(DateTime.Now);
            DateTime dtDate2 = objCronTab.GetNextOccurrence(dtDate1);
            TimeSpan ts = dtDate2.Subtract(dtDate1);
            int days = ts.Days;
            days = days * 7;//For safeside
            ArrayList alDate = new ArrayList();
            foreach (DateTime dtNew in objCronTab.GetNextOccurrences(dtDate.AddDays(-days), dtDate))
            {
                alDate.Add(dtNew);
            }
            for (int i = alDate.Count - 1; i >= 0; i--)
            {
                if (!((DateTime)alDate[i] >= dtDate))
                {
                    return (DateTime)alDate[i];
                }
            }
            return dtDate;
        }//juzer
    }
}