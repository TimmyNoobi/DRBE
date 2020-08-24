using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRBE
{
    public class Platform
    {
        public UInt32 ID = 0;

        public UInt64 Timestamp = 0;

        public Single LocationX = 0;
        public Single LocationY = 0;
        public Single LocationZ = 0;

        public Single VelocityX = 0;
        public Single VelocityY = 0;
        public Single VelocityZ = 0;

        public Single AccelerationX = 0;
        public Single AccelerationY = 0;
        public Single AccelerationZ = 0;

        public Single AngularVelocityX = 0;
        public Single AngularVelocityY = 0;
        public Single AngularVelocityZ = 0;

        public Single OrientationPsi = 0;
        public Single OrientationTheta = 0;
        public Single OrientationPhi = 0;





        public Single iLocationX = 0;
        public Single iLocationY = 0;
        public Single iLocationZ = 0;

        public Single iVelocityX = 0;
        public Single iVelocityY = 0;
        public Single iVelocityZ = 0;

        public Single iAccelerationX = 0;
        public Single iAccelerationY = 0;
        public Single iAccelerationZ = 0;

        public Single iAngularVelocityX = 0;
        public Single iAngularVelocityY = 0;
        public Single iAngularVelocityZ = 0;

        public Single iOrientationPsi = 0;
        public Single iOrientationTheta = 0;
        public Single iOrientationPhi = 0;

        public Single MaxSpeed = 0;
        public Single MaxAcceleration = 0;
        public Single MaxAngularSpeed = 0;


        public List<string> Property_value = new List<string>();
        public List<string> Property_string = new List<string>();
        public Platform(List<double> x)
        {
            //ID = x[0];

            //MaxSpeed = x[1];

            //iVelocityX = x[2];

            //iVelocityY = x[3];

            //iVelocityZ = x[4];

            Edit_pstring();
            Edit_pvalue();
        }

        private void Edit_pstring()
        {
            Property_string = new List<string>();
            Property_string.Add("ID:         ");
            Property_string.Add("Max Speed:  ");
            Property_string.Add("Velocity X: ");
            Property_string.Add("Velocity Y: ");
            Property_string.Add("Velocity Z: ");
            //Property_string.Add("Acceleration X");
            //Property_string.Add("Acceleration Y");
            //Property_string.Add("Acceleration Z");

        }

        private void Edit_pvalue()
        {
            Property_value.Add(ID.ToString());
            Property_value.Add(MaxSpeed.ToString());
            Property_value.Add(iVelocityX.ToString());
            Property_value.Add(iVelocityY.ToString());
            Property_value.Add(iVelocityZ.ToString());
        }
    }
}
