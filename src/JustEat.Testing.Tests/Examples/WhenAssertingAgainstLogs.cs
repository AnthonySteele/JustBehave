﻿using System.Linq;
using NLog;
using NLog.Layouts;
using NLog.Targets;
using Shouldly;
using Xunit;

namespace JustEat.Testing.Tests.Examples
{
    public class WhenAssertingAgainstLogs : XBehaviourTest<LoggingThing>
    {
        private string _message;

        [Then]
        public void ShouldBeAbleToAssertLogHappened()
        {
            ((MemoryTarget) LoggingTarget).Logs.SingleOrDefault(x => x.Equals(_message)).ShouldNotBe(null);
        }

        [Then]
        public void ShouldBeAbleToUsePredicateExtensionMethod()
        {
            LoggingTarget.ShouldHaveLogged(x => x == _message);
        }

        [Then]
        public void ShouldBeAbleToUseStringExtensionMethod()
        {
            LoggingTarget.ShouldHaveLogged(_message);
        }

        protected override LogLevel ConfigureLogLevel()
        {
            return LogLevel.Trace;
        }

        protected override TargetWithLayout ConfigureLoggingTarget()
        {
            return new MemoryTarget {Layout = LogLayout()};
        }

        protected override LoggingThing CreateSystemUnderTest()
        {
            return new LoggingThing {Log = Log};
        }

        protected override void Given()
        {
            _message = "some message";
        }

        protected override Layout LogLayout()
        {
            return "${message}";
        }

        protected override void When()
        {
            SystemUnderTest.Log.Debug(_message);
        }
    }

    public class LoggingThing
    {
        public Logger Log { get; set; }
    }
}
