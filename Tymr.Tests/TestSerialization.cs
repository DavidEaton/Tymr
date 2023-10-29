using Newtonsoft.Json;

namespace Tymr.Tests
{
    public class TestSerialization
    {
        [Fact]
        public void SerializeAndDeserializeTimeOnlyTest()
        {
            // Arrange
            var originalTimeOnly = new TimeOnly(12, 0);  // 12:00

            // Act
            string serializedTimeOnly = JsonConvert.SerializeObject(originalTimeOnly);
            TimeOnly deserializedTimeOnly = JsonConvert.DeserializeObject<TimeOnly>(serializedTimeOnly);

            // Assert
            Assert.Equal(originalTimeOnly, deserializedTimeOnly);
        }
    }
}