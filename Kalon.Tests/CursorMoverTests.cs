﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using Kalon.Tests.Native.PInvoke;
using Xunit;

namespace Kalon.Tests
{
    public sealed class CursorMoverTests
    {
        [Theory]
        [InlineData(0, 0, 20)]
        [InlineData(83, 27, 150)]
        [InlineData(27, 83, 880)]
        [InlineData(100, 100, 1200)]
        [InlineData(250, 400, 3500)]
        [InlineData(400, 250, 8000)]
        public void TestDelay(int x, int y, int milliseconds)
        {
            var point = new Point(x, y);

            var stopwatch = Stopwatch.StartNew();

            CursorMover.MoveCursor(point, TimeSpan.FromMilliseconds(milliseconds));

            stopwatch.Stop();

            Assert.InRange(stopwatch.ElapsedMilliseconds, milliseconds, milliseconds + 80);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(83, 27)]
        [InlineData(27, 83)]
        [InlineData(100, 100)]
        [InlineData(250, 400)]
        [InlineData(1000, 300)]
        public void TestMovement(int x, int y)
        {
            var point = new Point(x, y);

            CursorMover.MoveCursor(point, TimeSpan.FromMilliseconds(1000));

            if (!User32.GetCursorPos(out var currentCursorPosition))
            {
                throw new Win32Exception();
            }

            Assert.Equal(point, currentCursorPosition);
        }
    }
}