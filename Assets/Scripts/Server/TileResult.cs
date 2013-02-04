using System.Threading;

public class TileResult : ITileResult
{
	private int data;
	private int latency;
	private Thread thread;
	
	public bool IsDone {
		get;
		private set;
	}
	
	public int Data {
		get;
		private set;
	}
	
	public TileResult (int data, int latency)
	{
		this.data = data;
		this.latency = latency;
		this.Data = 0;
		this.IsDone = false;
		this.thread = new Thread (SimulateLatency);
		this.thread.Start ();
	}
	
	private void SimulateLatency ()
	{
		Thread.Sleep (latency);
		
		IsDone = true;
		Data = data;
	}
}