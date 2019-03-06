function bar() {   console.log(this.a); } 
var obj2 = {    a: 42,    bar: bar }; 
var obj1 = {    a: 2,    obj2: obj2 } 

obj2.bar();
obj1.obj2.bar();
