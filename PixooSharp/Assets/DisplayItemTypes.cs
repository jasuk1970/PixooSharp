using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Assets
{
    public enum DisplayItemTypes
    {
        SECOND = 1,           //sceocnd , font should include digit
        MIN = 2,              //min, font should include digit
        HOUR = 3,             //hour, font should include digit
        TIME_AM_PM = 4,      //am or pm, font should include a,m,p
        HOUR_MIN = 5,         //hour：min , font should include digit
        HOUR_MIN_SEC = 6,     //hour:min:sec, , font should include digit
        YEAR = 7,             //year,, font should include digit
        DAY = 8,              //day, font should include digit
        MON = 9,              //month, font should include digit
        MON_YEAR = 10,         //mon-year, font should include digit
        ENG_MONTH_DOT_DAY = 11,//month, font should include english letters
        DATE_WEEK_YEAR = 12,   //day:month:year, font should include digit
        ENG_WEEK = 13,         ///weekday-"SU","MO","TU","WE","TH","FR","SA", font should include english letters
        ENG_WEEK_THREE = 14,         //weekday-"SUN","MON","TUE","WED","THU","FRI","SAT", font should include english letters
        ENG_WEEK_ALL = 15,         //weekday-"SUNDAY","MONDAY","TUESDAY","WEDNESDAY","THURSDAY","FRIDAY","SATURDAY", font should include english letters
        ENG_MON = 16,         //month-"JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC", font should include english letters
        TEMP_DIGIT = 17,         //temperature, font should include digit and c,f
        TODAY_MAX_TEMP = 18,         //the max temperature, font should include digit and c,f
        TODAY_MIN_TEMP = 19,        //the min temperature, font should include digit and c,f
        WEATHER_WORD = 20,         //the weather, font should include english letters
        NOISE_DIGIT = 21,          //the nosie value, font should include digit 
        TEXT_MESSAGE = 22,   //the text string, font should include text information
        NET_TEXT_MESSAGE = 23,   //the url request string, font 
    }
}
