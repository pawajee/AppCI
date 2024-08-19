import { from, take } from 'rxjs';

function* generateDoubles(seed) {
   let i = seed;
   while (true) {
     yield i;
     i = 2 * i; // double it
   }
}

const iterator = generateDoubles(3);
const result = from(iterator).pipe(take(10));

result.subscribe(x => console.log(x));

// Logs:
// 3
// 6
// 12
// 24
// 48
// 96
// 192
// 384
// 768
// 1536

///////////////////////////////////////////////

console.log('start');
 
const array = [10, 20, 30];
const result = from(array, asyncScheduler);
 
result.subscribe(x => console.log(x));
 
console.log('end');
 
// Logs:
// 'start'
// 'end'
// 10
// 20
// 30
