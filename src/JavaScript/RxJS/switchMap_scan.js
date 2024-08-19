// RxJS v6+
import { interval, fromEvent, merge, empty } from 'rxjs';
import {
  switchMap,
  scan,
  takeWhile,
  startWith,
  mapTo,
  tap,
} from 'rxjs/operators';

const COUNTDOWN_SECONDS = 10;
//let fnDelegate= (acc, curr) =>  (curr ? curr + acc : acc), 10);
// elem refs
const remainingLabel = document.getElementById('remaining');
const pauseButton = document.getElementById('pause');
const resumeButton = document.getElementById('resume');

// streams
const interval$ = interval(1000).pipe(mapTo(-1));
const pause$ = fromEvent(pauseButton, 'click').pipe(mapTo(false));
const resume$ = fromEvent(resumeButton, 'click').pipe(mapTo(true));

const timer$ = merge(pause$, resume$)
  .pipe(
    tap((_) => console.log(`first in pipe:${_}`)),
    startWith(false),
    switchMap((val) => (val ? interval$ : empty())),
    // scan((acc, curr) => (curr ? curr + acc : acc), COUNTDOWN_SECONDS),
    scan((acc, curr) => {
      console.log(`${curr} : ${acc} : ${curr + acc}`);
      var res = curr ? curr + acc : acc;
      console.log(`${res}`);
      return res;
    }, COUNTDOWN_SECONDS),
    tap((_) => console.log(`${_}`)),
    takeWhile((v) => v >= 0)
  )
  .subscribe((val: any) => (remainingLabel.innerHTML = val));

/*
    scan((acc, curr) => {
      debugger;
      console.log(`${curr} - ${acc} - ${curr + acc}`);
      var res = curr ? curr + acc : acc ;
      console.log(`${res}`);
      return res;
    },COUNTDOWN_SECONDS),
    */
