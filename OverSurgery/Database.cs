using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverSurgery
{
    class Database
    {
        public bool VerifyPassword         //this will compare the password given and the one stored and return true or false. 
        {

        }

        public bool VerifyPatientsID           //this will compare the ID given and the one stored and return true or false. 
        {

        }

        public object GetBooking           //retrieves the patients booking if exists and stores the values in a object
        {


        }

        
        public void RecordBooking            //record the patients booking to the database
        {

        }


        public void DeleteBooking          //Deletes the patients booking from the database
        {


        }


        public object GetMedication         //Retrieves all the medication  details of a specific patient  and stores them in a object
        {

        }

       

        public void RecordMedication           //Records the new medication entry to the database
        {

        }

  

        public object GetTestResults            //retrieves the patientTest results booking if exists and stores the values in a object
        {

        }

        public void RecordTestResults           //Records the test results to the database
        {

        }

        public object GetTimetable          //Retrieves the booked timetable slots and stores them in a object
        {

        }

    
        public object GetStaff         //retrieves the staff's entry and stores it to a object
        {

        }

        public void RecordStaff            //record the staff entry to the database
        {

        }

        public void DeleteStaff            //deletes the staff's booking from the database
        {

        }

     

        public object GetWeekX         //retrieves a list of list of staff names entry and stores it to a object
        {

        }

        public void RecordWeekX            //Record the values of week x to the database            
        {

        }

        public object GetPatientDetails        //retrieves a list of list of staff names entry and stores it to a object
        {

        }

      

        public void RecordPatientDetails            //Records the patient details to the database
        {

        }


        public object GetStaffAvailability          //retrieves the appointments booked uppon request and stores the values in a object
        {

        }


    }

}
