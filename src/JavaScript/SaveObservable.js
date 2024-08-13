//https://stackblitz.com/edit/rxjs-nrwwbl?devtoolsheight=60&file=index.ts,index.html
import { Observable,fromEvent,from,of } from 'rxjs';
import { tap, map,concatMap } from 'rxjs/operators';

var saveBtn=document.querySelector('#saveBtn');
fromEvent(saveBtn, 'click')
  .pipe(
    concatMap(click => save())
      )
  .subscribe(result => {
    
    console.log(`result:${typeof result}:${result}`);
    // result is a stream!
  });
/*
  var savePrm=new Promise(
    (res, reject)=>{
      //console.log("promise called");
      setTimeout(()=>{
        console.log("solving the promise");
        res("save..");
      }
        ,3000
      );
  });
 
 function save(){ 
   console.log('inside save..');
   return from(savePrm);
 }
  */
   //from(savePrm);
  
function save(){
  console.log(`inside save..`);
  return new Observable<string>((observer:any)=> {
    let count = 0;
  
    setTimeout(() => {
      observer.next( "saved...");
      observer.complete();
    }, 1000);
  });
}
