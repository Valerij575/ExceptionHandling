# Practical ASP.NET Web API

Sigmar business tool.

## 2. Debugging and Tracing

### 2.1 Enabling ASP.Net Web API Tracing

In this exercise, you will enable ASP.NET Web API tracing using System.Diagnostics. 
You will use the NuGet package Microsoft ASP.NET Web API Tracing.

1. Run Visual Studio and open the project corresponding to Exercise 1.2. 
	Alternatively, you can create a new ASP.NET MVC 4 project with the Web API template, add a new
	ApiController with a name of EmployeesController

2. In the Package Managenent Console enter
	Install-Package Microsoft.AspNet.WebApi.Tracing

3. In the Register method of WebApiConfig, in the App_Start folder, add the following line
	of code:
	<code>config.EnableSystemDiagnosticsTracing();</code>

### 2.2 Creating a Custom Trace Writer

1. Create new class WebApiTracer implementing ITraceWriter

2. To implement ITraceWriter, we must implement the Trace method. The logic of the
Trace method is as follows.
	a. Create a new TraceRecord object.
	b. Invoke the caller’s traceAction, passing the TraceRecord object.
	c. Write XML out based on the TraceRecord object with details filled in by the caller.
	d. I use C:\path\log.xml. You will need to adjust this path to get the XML file created in a valid path in your computer.

3. To plug the trace writer in, add a line of code in the Register method of WebApiConfig under App_Start folder, like so:

	<code>config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());</code>

	this line appears after the line <code>config.EnableSystemDiagnosticsTracing();</code>

4. Rebuild the solution and issue a request to the web API (it can be any request).
5. Open the XML file and review the contents

### 2.3 Tracing Entry and Exit

1. Create a new trace writer with a name of EntryExitTracer
	The trace writer just stores the TraceRecord object with details in a ring buffer, which is our custom implementation.

2. Create a new RingBufferLog class. 
	It is a singleton class, and it stores the TraceRecord objects in a fixed-size array. The basic idea is to ensure that only
	TraceRecord objects of a fixed size are stored, and the older items are evicted to make way for the newer items. When an item is queued, 
	it moves the pointer by one and stores the item in that position. 
	If the pointer reaches the end of the array, it is reset to 0. Since multiple threads can queue, 
	we use lock to handle concurrency. PeekAll returns the buffer content, and DequeueAll drains the buffer.

3. To plug the trace writer in, add a line of code in the Register method of WebApiConfig under App_Start folder.
4. Add a new ApiController with a name of TracesController.
5. Make GET request to ../api/traces. It displays the trace with time in milliseconds.