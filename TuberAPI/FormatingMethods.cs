using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI
{
    public class FormatingMethods
    {
        public static string getCorrectDateTimeFormat(string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy HH:mm:ss";
            string bookingRequestDate = date;
            
            try{
                DateTime dt = DateTime.ParseExact(bookingRequestDate, format, provider);
                string result = "" + dt;
                bookingRequestDate = result;
                return bookingRequestDate;
            }
            catch (FormatException)
            {
                format = "MM/dd/yyyy HH:mm:ss tt";

                try
                {
                    DateTime dt = DateTime.ParseExact(bookingRequestDate, format, provider);
                    string result = "" + dt;
                    bookingRequestDate = result;
                    return bookingRequestDate;
                }
                catch (FormatException)
                {
                    format = "dd/MM/yyyy HH:mm:ss";
                    try
                    {
                        DateTime dt = DateTime.ParseExact(bookingRequestDate, format, provider);
                        string result = "" + dt;
                        bookingRequestDate = result;
                        return bookingRequestDate;
                    }
                    catch (FormatException)
                    {
                        format = "dd/MM/yyyy HH:mm:ss tt";

                        try
                        {
                            DateTime dt = DateTime.ParseExact(bookingRequestDate, format, provider);
                            string result = "" + dt;
                            bookingRequestDate = result;
                            return bookingRequestDate;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} is not in the correct format.", bookingRequestDate);
                            return date;
                        }
                    }
                }

            }
        }
    }
}
