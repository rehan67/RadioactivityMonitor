namespace RadioactivityMonitor.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Alarm"/> covering normal and boundary behaviors.
    /// </summary>
    public class AlarmTests
    {
        private sealed class StubSensor : ISensor
        {
            private readonly double _value;
            public StubSensor(double value)
            {
                _value = value;
            }
            public double NextMeasure()
            {
                return _value;
            }
        }

        /// <summary>
        /// Given a value within the expected range, when Check is called, then the alarm remains off.
        /// </summary>
        [Fact]
        public void GivenValueWithinRange_WhenCheck_ThenAlarmOff()
        {
            // Arrange
            var sensor = new StubSensor(18.5);
            var alarm = new Alarm(sensor);

            // Act
            alarm.Check();

            // Assert
            Assert.False(alarm.AlarmOn);
        }

        /// <summary>
        /// Given a value above the upper threshold, when Check is called, then the alarm turns on.
        /// </summary>
        [Fact]
        public void GivenValueAboveUpperThreshold_WhenCheck_ThenAlarmOn()
        {
            // Arrange
             var sensor = new StubSensor(22.0);
            var alarm = new Alarm(sensor);

            // Act
            alarm.Check();

            // Assert
            Assert.True(alarm.AlarmOn);
        }

        /// <summary>
        /// Given a value below the lower threshold, when Check is called, then the alarm turns on.
        /// </summary>
        [Fact]
        public void GivenValueBelowLowerThreshold_WhenCheck_ThenAlarmOn()
        {
            // Arrange
            var sensor = new StubSensor(16.9);
            var alarm = new Alarm(sensor);

            // Act
            alarm.Check();

            // Assert
            Assert.True(alarm.AlarmOn);
        }

        /// <summary>
        /// Given a value exactly equal to the lower threshold, when Check is called, then the alarm remains off.
        /// </summary>
        [Fact]
        public void GivenValueEqualToLowerThreshold_WhenCheck_ThenAlarmOff()
        {
            // Arrange
            var sensor = new StubSensor(17.0);
            var alarm = new Alarm(sensor);

            // Act
            alarm.Check();

            // Assert
            Assert.False(alarm.AlarmOn);
        }

        /// <summary>
        /// Given a value exactly equal to the upper threshold, when Check is called, then the alarm remains off.
        /// </summary>
        [Fact]
        public void GivenValueEqualToUpperThreshold_WhenCheck_ThenAlarmOff()
        {
            // Arrange
            var sensor = new StubSensor(21.0);
            var alarm = new Alarm(sensor);

            // Act
            alarm.Check();

            // Assert
            Assert.False(alarm.AlarmOn);
        }
    }
}
