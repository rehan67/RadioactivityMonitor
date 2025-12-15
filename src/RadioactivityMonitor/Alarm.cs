namespace RadioactivityMonitor
{
    public class Alarm(ISensor? sensor = null)
    {
        private const double LowThreshold = 17;
        private const double HighThreshold = 21;

        private readonly ISensor _sensor = sensor ?? new Sensor();
        private bool _alarmOn = false;
        private long _alarmCount = 0;

        public void Check()
        {
            double value = _sensor.NextMeasure();

            if (value < LowThreshold || value > HighThreshold)
            {
                _alarmOn = true;
                _alarmCount += 1;
            }
            else
            {
                _alarmOn = false;
            }
        }

        public bool AlarmOn => _alarmOn;
        public long AlarmCount => _alarmCount;
    }
}
