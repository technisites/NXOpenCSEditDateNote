using NXOpen;
using NXOpen.Annotations;
using NXOpen.UF;
using NXOpenUI;
using TechniLib;
using System;


static class Reformat_current_date
{
    private static Session s = Session.GetSession();
    private static UFSession ufs = UFSession.GetUFSession();
    private static ListingWindow lw = s.ListingWindow;
    private static Selection sel = UI.GetUI().SelectionManager;

    public static void Main()
    {
        Session.UndoMarkId markId1;
        markId1 = s.SetUndoMark(Session.MarkVisibility.Visible, "EditNoteDate");

        Note theNote = null;

        if ( sel.GetNumSelectedObjects() > 0 )
        {
            theNote = (Note)sel.GetSelectedTaggedObject(0);
        }
        else
            theNote = TechniLib.TechniSelect.SelectANote("Select Date Note");

        if (theNote == null)
            return;

        NXOpen.UF.SystemInfo sysInfo;
        ufs.UF.AskSystemInfo(out sysInfo);

        lw.Open();
        lw.WriteLine("Default date and time format: " + sysInfo.date_buf);

        string theYear = sysInfo.date_buf.Substring(2, 2);
        string theMonth = sysInfo.date_buf.Substring(5, 2);
        string theDay = sysInfo.date_buf.Substring(8, 2);
        string theTime = sysInfo.date_buf.Substring(11);

        string DayMonthYear = theDay + "." + theMonth + "." + theYear;
        string MonthDayYear = theMonth + "-" + theDay + "-" + theYear;

        //lw.WriteLine("Day-Month-Year: " + DayMonthYear);

        //lw.WriteLine("Month-Day-Year: " + MonthDayYear);

        string[] theNoteText = { DayMonthYear };

        theNote.SetText(theNoteText);

        int nErrs1;
        nErrs1 = s.UpdateManager.DoUpdate(markId1);

    }


    public static int GetUnloadOption(string dummy)
    {
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
    }
}


