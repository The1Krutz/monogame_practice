using System;
using Xunit;

namespace nez_pong.Test {
    public class UnitTest1 {
        [Fact]
        public void Test1() {
            int x = 5;
            Assert.Equal(5, x);
        }

        [Fact]
        public void Test2() {
            int x = 5;
            Assert.Equal(3, x);
        }
    }
}
