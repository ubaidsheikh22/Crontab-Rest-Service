using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NCrontab;
using RestService.Models;
using System.Collections;

namespace RestService.Controllers
{
    [RoutePrefix("api")]
    public class CrontabController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        [Route("Crontab/GetDate")]
        public IHttpActionResult GetDate(Parameters pr)
        {
            try
            {

                DateTime MIN = new DateTime();
                Frequency obj_fre = new Frequency();
                CrontabSchedule objsc = CrontabSchedule.Parse(pr.Date_string);
                GetVariables gv = new GetVariables();
                if (pr.Cycle == "C")
                {
                    DateTime MAX = obj_fre.getPreviousOccurance2(objsc, pr.Date);
                    MIN = obj_fre.getPreviousOccurance2(objsc, MAX);
                    if (pr.Freq != "2")
                    {

                        if (pr.Datewise)
                            MIN = objsc.GetNextOccurrence(Convert.ToDateTime(MIN).AddMonths(-1));

                    }
                }
                else if (pr.Cycle == "P")
                {
                    MIN = obj_fre.getPreviousOccurance2(objsc, pr.Date);
                    if (pr.Freq != "2")
                    {

                        if (pr.Datewise)
                            MIN = obj_fre.getPreviousOccurance2(objsc, MIN);

                    }

                }
                else if (pr.Cycle == "S")
                {
                    MIN = pr.Date;
                }
                else if (pr.Cycle == "N")
                {
                    MIN = objsc.GetNextOccurrence(Convert.ToDateTime(pr.Date));
                    if (MIN.Day != 1)
                    {
                        MIN = objsc.GetNextOccurrence(Convert.ToDateTime(MIN));
                    }
                    // date.ToString("MMM dd, yyyy hh:mm tt")
                    MIN = objsc.GetNextOccurrence(Convert.ToDateTime(MIN));
                    MIN = obj_fre.getPreviousOccurance2(objsc, Convert.ToDateTime(MIN));
                    if (pr.Freq != "2")
                    {

                        if (pr.Datewise)
                            MIN = objsc.GetNextOccurrence(Convert.ToDateTime(MIN).AddMonths(-1));

                    }

                }
                if (pr.ShowDate !="")
                {
                    MIN = Convert.ToDateTime(pr.ShowDate);
                }

                if (pr.Freq == "5")
                {
                    decimal j = 0;
                    decimal month = Convert.ToDateTime(MIN).Month;
                    j = month / 3;
                    j = Math.Ceiling(j);
                    //FormatedString = "Q" + j + " " + Convert.ToDateTime(MIN).ToString("yyyy"); //+ " " + Convert.ToDateTime(MIN).ToString("MMM yyyy");
                    if (pr.Datewise)
                    {
                        gv.FormatedString = String.Format("{0:y}", objsc.GetNextOccurrence(MIN).AddMonths(-1));
                        gv.FormatedDate = formatDate(objsc.GetNextOccurrence(MIN).AddMonths(-1));
                    }
                    else
                    {
                        gv.FormatedString = "Q" + j + " " + Convert.ToDateTime(MIN).ToString("yyyy"); // + " "  + Convert.ToDateTime(MIN).ToString("MMM yyyy");
                        gv.FormatedDate = formatDate(MIN);
                    }
                }

                else if (pr.Freq == "3")
                {

                    gv.FormatedString = "Y" + " " + Convert.ToDateTime(MIN).ToString("MMM yyyy");
                    gv.FormatedDate = formatDate(Convert.ToDateTime(MIN));

                }
                else if (pr.Freq == "6")
                {
                    decimal j = 0;
                    decimal month = Convert.ToDateTime(MIN).Month;
                    j = month / 6;
                    j = Math.Ceiling(j);
                    gv.FormatedString = "HY" + j + " " + Convert.ToDateTime(MIN).ToString("yy");// + " " + Convert.ToDateTime(MIN).ToString("MMM yyyy");
                    gv.FormatedDate = formatDate(MIN);
                }
                else if (pr.Freq == "2")
                {
                    gv.FormatedString = String.Format("{0:y}", MIN);
                    //  formatDate(Convert.ToDateTime(MIN));
                    gv.FormatedDate = formatDate(Convert.ToDateTime(MIN));
                }
                else
                {
                    gv.FormatedString = String.Format("{0:y}", MIN);
                    gv.FormatedDate = formatDate(Convert.ToDateTime(MIN));
                }

                return Ok(gv);
            }
            catch (Exception ex)
            {
                return Ok(BadRequest());
            }
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        [Route("Crontab/getPreviousOccurance")]
        public IHttpActionResult getPreviousOccurance(Parameters pr)
        {
            try
            {

                CrontabSchedule objCronTab = CrontabSchedule.Parse(pr.Date_string);
                DateTime dtDate1 = objCronTab.GetNextOccurrence(DateTime.Now);
                DateTime dtDate2 = objCronTab.GetNextOccurrence(dtDate1);
                TimeSpan ts = dtDate2.Subtract(dtDate1);
                int days = ts.Days;
                days = days * 7;//For safeside
                ArrayList alDate = new ArrayList();
                foreach (DateTime dtNew in objCronTab.GetNextOccurrences(pr.Dtdate.AddDays(-days), pr.Dtdate.AddSeconds(1)))
                {
                    alDate.Add(dtNew);
                }
                for (int i = alDate.Count - 1; i > 0; i--)
                {
                    if (!((DateTime)alDate[i] > pr.Dtdate))
                    {
                        return Ok((DateTime)alDate[i]);
                    }
                }
                return Ok(pr.Dtdate);
            }
            catch(Exception ex)
            {
                return Ok(BadRequest());
            }
            }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        [Route("Crontab/getPreviousOccurance2")]
        public IHttpActionResult getPreviousOccurance2(Parameters pr)
        {
            try
            {
                CrontabSchedule objCronTab = CrontabSchedule.Parse(pr.Date_string);
                DateTime dtDate1 = objCronTab.GetNextOccurrence(DateTime.Now);
                DateTime dtDate2 = objCronTab.GetNextOccurrence(dtDate1);
                TimeSpan ts = dtDate2.Subtract(dtDate1);
                int days = ts.Days;
                days = days * 7;//For safeside
                ArrayList alDate = new ArrayList();
                foreach (DateTime dtNew in objCronTab.GetNextOccurrences(pr.Dtdate.AddDays(-days), pr.Dtdate))
                {
                    alDate.Add(dtNew);
                }
                for (int i = alDate.Count - 1; i >= 0; i--)
                {
                    if (!((DateTime)alDate[i] >= pr.Dtdate))
                    {
                        return Ok((DateTime)alDate[i]);
                    }
                }
                return Ok(pr.Dtdate);
            }
            catch(Exception ex)
            {
                return Ok(BadRequest());
            }
            }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        [Route("Crontab/getNextOccurrence")]
        public IHttpActionResult getNextOccurrence(Parameters pr)
        {
            try
            {
                CrontabSchedule objCronTab = CrontabSchedule.Parse(pr.Date_string);
                return Ok(objCronTab.GetNextOccurrence(pr.Dtdate));
            }
            catch(Exception ex)
            {
                return Ok(BadRequest());
            }
        }


        public class GetVariables
        {
            public string FormatedString = "";
            public string FormatedDate = "";
        }
        public string formatDate(DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }
    }
   
}

