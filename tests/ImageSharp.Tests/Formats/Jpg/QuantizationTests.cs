// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using SixLabors.ImageSharp.Formats.Jpeg.Components;
using Xunit;
using Xunit.Abstractions;

using JpegQuantization = SixLabors.ImageSharp.Formats.Jpeg.Components.Quantization;

namespace SixLabors.ImageSharp.Tests.Formats.Jpg
{
    [Trait("Format", "Jpg")]
    public class QuantizationTests
    {
        public QuantizationTests(ITestOutputHelper output)
        {
            this.Output = output;
        }

        private ITestOutputHelper Output { get; }

        //[Fact(Skip = "Debug only, enable manually!")]
        [Fact]
        public void PrintVariancesFromStandardTables_Luminance()
        {
            this.Output.WriteLine("Variances for Luminance table.\nQuality levels 25-100:");

            double minVariance = double.MaxValue;
            double maxVariance = double.MinValue;

            for (int q = JpegQuantization.QualityEstimationConfidenceThreshold; q <= JpegQuantization.MaxQualityFactor; q++)
            {
                Block8x8F table = JpegQuantization.ScaleLuminanceTable(q);
                double variance = JpegQuantization.EstimateQuality(ref table, JpegQuantization.UnscaledQuant_Luminance, out double quality);

                minVariance = Math.Min(minVariance, variance);
                maxVariance = Math.Max(maxVariance, variance);

                this.Output.WriteLine($"q={q}\t{variance}\test. q: {quality}");
            }

            this.Output.WriteLine($"Min variance: {minVariance}\nMax variance: {maxVariance}");
        }

        //[Fact(Skip = "Debug only, enable manually!")]
        [Fact]
        public void PrintVariancesFromStandardTables_Chrominance()
        {
            this.Output.WriteLine("Variances for Chrominance table.\nQuality levels 25-100:");

            double minVariance = double.MaxValue;
            double maxVariance = double.MinValue;
            for (int q = JpegQuantization.QualityEstimationConfidenceThreshold; q <= JpegQuantization.MaxQualityFactor; q++)
            {
                Block8x8F table = JpegQuantization.ScaleChrominanceTable(q);
                double variance = JpegQuantization.EstimateQuality(ref table, JpegQuantization.UnscaledQuant_Chrominance, out double quality);

                minVariance = Math.Min(minVariance, variance);
                maxVariance = Math.Max(maxVariance, variance);

                this.Output.WriteLine($"q={q}\t{variance}\test. q: {quality}");
            }

            this.Output.WriteLine($"Min variance: {minVariance}\nMax variance: {maxVariance}");
        }
    }
}
