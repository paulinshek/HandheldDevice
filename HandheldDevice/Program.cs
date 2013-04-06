using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.Seeed;
using Gadgeteer.Modules.GHIElectronics;

namespace HandheldDevice
{
    public partial class Program
    {
        enum State {ON,OFF};
        State deviceState;
        GT.Timer timer;
        Computation c = new Computation();

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/

            button.ButtonPressed += new Button.ButtonEventHandler(button_ButtonPressed);

            gyro.Calibrate();

            timer = new GT.Timer(250);
            timer.Tick += new GT.Timer.TickEventHandler(timer_Tick);

            gyro.MeasurementComplete += new Gyro.MeasurementCompleteEventHandler(gyro_MeasurementComplete);

            deviceState = State.ON ;
        }

        void timer_Tick(GT.Timer timer)
        {
            gyro.RequestMeasurement();
            //also need to get measurement from IR sensor
        }

        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            if (deviceState == State.ON)
            {
                timer.Stop();
                deviceState = State.OFF;
            }
            else
            {
                timer.Start();
                deviceState = State.ON;
                //also need to stop vibration and sound, if they are doing something?
            }

        }

        void gyro_MeasurementComplete(Gyro sender, Gyro.SensorData sensorData)
        {
            c.addLocation(sensorData.X,sensorData.Y,sensorData.Z);
        }
    }
}
