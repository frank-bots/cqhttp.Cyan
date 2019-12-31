using System;
using Xunit;

namespace cqhttp.Cyan.Utils {
    public class LoggerTest {
        [Fact]
        public void Test () {
            Logger a = Logger.GetLogger ("a");
            a.log_level = Enums.Verbosity.OFF;
            Assert.Equal (
                Enums.Verbosity.OFF,
                Logger.GetLogger ("a").log_level
            );
        }
    }
}