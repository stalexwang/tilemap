/// <summary>
/// Simulate http request that has latency.
/// The Data is only valid when IsDone is true.
/// </summary>
public interface ITileResult
{
	bool IsDone{ get; }

	int Data{ get; }
}