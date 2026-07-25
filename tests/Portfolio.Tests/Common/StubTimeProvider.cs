namespace Portfolio.Tests.Common;

/// <summary>Часы с фиксированным значением — чтобы тесты не зависели от реального времени.</summary>
internal sealed class StubTimeProvider(DateTimeOffset now) : TimeProvider
{
    public override DateTimeOffset GetUtcNow() => now;
}
