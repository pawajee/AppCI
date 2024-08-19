import { fromEvent } from 'rxjs'; 
import { map, buffer, filter, debounceTime } from 'rxjs/operators';


const click$ = fromEvent(document, 'click');

const doubleClick$ = click$
.pipe(
  buffer(click$.pipe(debounceTime(250))),
  map(clicks => clicks.length),
  filter(clicksLength => clicksLength >= 2)
);

doubleClick$.subscribe(_ => {
  console.log('double clicked detected', _)
});
