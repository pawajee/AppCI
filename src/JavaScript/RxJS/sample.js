import { interval } from 'rxjs';
import { sample,tap } from 'rxjs/operators';

//emit value every 1s
const source = interval(500);
//sample last emitted value from source every 2s
const example = source.pipe(tap(x=>console.log(x)),sample(interval(2000)));
//output: 2..4..6..8..
const subscribe = example.subscribe(val => console.log(val));
