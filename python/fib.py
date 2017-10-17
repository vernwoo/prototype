import sys;
def fib(n):
       if n==0 :
        return 0;
       if n==1 :
        return 1;
       return fib(n-1) + fib(n-2);

fn = 0;  
if len(sys.argv) > 1: 
    fn = int(sys.argv[1]);
else:
    fn = int(input("Enter a number for its Febonacci value:"));

print(fib(fn));
