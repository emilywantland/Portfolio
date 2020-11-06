// Grading ID: N2466
// Section: CIS 199-75
// Lab #: 9
// Due Date: 4-21-2019
// Description: When a valid month, day, or year is entered--the form's displayed date should change. 

using System.Windows.Forms;

namespace Lab9
{
    public class Date
    {
        // Declaring variables
        private int _month; // 1-12
        private int _day;   // 1-31
        private int _year;  // >= 0
        
        // Precondition:  1 <= m <= 12
        //                1 <= d <= 31
        //                0 <= y
        // Postcondition: The date is changed to the specified month,
        //                day, and year
        public Date(int m = 0, int d = 0, int y = 0)
        {
            Month = m; // Set the month property
            Day = d;   // Set the day property
            Year = y;  // Set the year property
        }
     
        // Month section
        public int Month
        {
            // Precondition:  None
            // Postcondition: The month has been returned
            get
            {
                return _month;
            }
            
            // Precondition:  0 <= value <= 12
            // Postcondition: The month has been set to the specified value
            set
            {
                if (value >= 1 && value <= 12)
                {
                    _month = value;
                }
                else // When invalid, set to 1 instead
                {
                    _month = 1;
                }
            } 
        }
        
        // Day Section
        public int Day
        {
            // Precondition:  None
            // Postcondition: The day has been returned
            get
            {
                return _day;
            }
            
            // Precondition:  0 <= value <= 31
            // Postcondition: The day has been set to the specified value
            set
            {
                if (value >= 1 && value <= 31)
                {
                    _day = value;
                }
                else // When invalid, set to 1 instead
                {
                    _day = 1;
                }
            }
        }
        
        // Year section
        public int Year
        {
            // Precondition:  None
            // Postcondition: The year has been returned
            get
            {
                return _year;
            }
            
            // Precondition:  0 <= value
            // Postcondition: The year has been set to the specified value
            set
            {
                if (value >= 0)
                {
                    _year = value;
                }
                else // When invalid, set to 2019 instead
                {
                    _year = 2019;
                }
            }
        }
        
        // Precondition:  None
        // Postcondition: A string is returned presenting the date in US format
        public override string ToString() // Override is required!
        {
            string result; // Builds result in steps
            
            result = $"{Month:D2}/{Day:D2}/{Year:D4}";
         
            return result;
        }
    }
}