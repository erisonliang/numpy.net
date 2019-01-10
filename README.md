# numpy.net
A port of NumPy to .Net

History:

In early 2018, I was tasked with porting a python/numpy application to .NET. 
There was no documentation available on the python source and I needed to 
produce binary compatible output.

I started out by trying to write custom code to do the numpy manipulations 
but that quickly became too difficult, so I searched for any solution that 
would allow .NET to perform numpy operations.  I ran across this public repository.   
I believe it is an abandoned effort to wrap the underlying C based numpy code in 
.NET interop code. It was originally worked on by Enthought Corporation under 
contract to Microsoft. I am not sure of the reasons, but this project was 
abandoned before it was finished. I was not able to figure out how to use this 
repository in any way, but that may have been user error on my part.

https://github.com/numpy/numpy-refactor 

My initial goal was to review this source for some tips on how the numpy 
manipulations were done so that I could use it in my custom code.  

I was getting desperate, so on a whim, I grabbed a couple of the C functions 
and tried to port them to C#.  Generally, I did a global replace of NULL to null 
nd -> to . and then had to convert pointers to class references and for the most 
part it compiled. I slowly converted the dozens of C macros to C# functions.  
Eventually I was able to get something to work so I started doing a few more 
functions.  Before long I had most of the C code ported into a .NET DLL.

Next, I took the C# code from this repository and tried to connect it to this 
newly ported DLL.  This involved unwinding the enormous amount of C interop 
code that the Enthought team wrote.   Since both the DLL and higher layer
library were now in C#, lots of things became simpler and it became easy to 
step through the code and debug it.  It took several months to port this 
code and make it work correctly.  

I ported enough of this code to complete my original porting task and then I
added many more functions to help complete the testing.
As a result, I believe I have a pure .NET numpy whose core logic is 
largely complete and correct.

Cool features:

python is a scripted language and C# is compiled language.  Much of the 
numpy syntax is not compilable in C#, especially the slicing syntax.
As the next best thing, I turned the python slicing sytax into a 
string which is parsed into compatible slice values.  For example:

in python, a statement like this:

	index = 2
	a = arr[:,:index]

	becomes a C# statement like this:
	
	ndarray a = arr[":", ":" + index.ToString()] as ndarray;
	
	or the shortcut form where casting to ndarray is not necessary:
	
	ndarray a = arr.A(":", ":" + index.ToString());




Unit tests:

I have unit tests in python and I have similarly named unit tests in C#.
I grabbed the generated output from the python tests and assert that the C# 
version produces the same results.

If the unit tests in the C# code are set for ignore, it is because they
currently do not work 100% correctly and need to be debugged.


