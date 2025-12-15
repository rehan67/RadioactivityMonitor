namespace RadioactivityMonitor.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Alarm"/> covering normal and boundary behaviors.
    /// </summary>
    public class AlarmTests
    {
        private sealed class StubSensor(double value) : ISensor
        {
            private readonly double _value = value;

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

        /// <summary>
        /// Given alarm triggered then normal value, when Check is called, then alarm resets.
        /// </summary>
        [Fact]
        public void GivenAlarmTriggeredThenNormalValue_WhenCheck_ThenAlarmResets()
        {
            // Arrange
            var alarm = new Alarm(new StubSensor(22.0));
            alarm.Check();
            Assert.True(alarm.AlarmOn);

            // Act
            alarm = new Alarm(new StubSensor(18.0));
            alarm.Check();

            // Assert
            Assert.False(alarm.AlarmOn);
        }

        /// <summary>
        /// Given null sensor, when alarm is created, then uses default sensor.
        /// </summary>
        [Fact]
        public void GivenNullSensor_WhenAlarmCreated_ThenUsesDefaultSensor()
        {
            // Arrange & Act
            var alarm = new Alarm(null);

            // Assert
            alarm.Check();
        }

        /// <summary>
        /// Given multiple alarm triggers, when Check is called, then alarm count increases.
        /// </summary>
        [Fact]
        public void GivenMultipleAlarmTriggers_WhenCheck_ThenAlarmCountIncreases()
        {
            // Arrange
            var alarm = new Alarm(new StubSensor(22.0));

            // Act
            alarm.Check();
            alarm.Check();

            // Assert
            Assert.Equal(2, alarm.AlarmCount);
        }

        /// <summary>
        /// Given extreme values, when Check is called, then correct alarm state is set.
        /// </summary>
        [Theory]
        [InlineData(0.0, true)]
        [InlineData(16.99, true)]
        [InlineData(21.01, true)]
        [InlineData(100.0, true)]
        public void GivenExtremeValues_WhenCheck_ThenCorrectAlarmState(double value, bool expectedAlarm)
        {
            // Arrange
            var alarm = new Alarm(new StubSensor(value));

            // Act
            alarm.Check();

            // Assert
            Assert.Equal(expectedAlarm, alarm.AlarmOn);
        }
    }
}
