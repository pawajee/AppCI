// Depends on these script files being available:
//    /library/whammy.js
//    /library/libwebp-0.1.3.min.js

// Some details here: https://stackoverflow.com/questions/52371970/changing-quality-of-mediarecorder-and-canvas-capturestream/52378272#52378272
// videoUrl must either be a blob object url, or a file hosted on the same domain as this script, or have proper CORS headers to prevent canvas being "tainted".

// Obviously this function is useless unless you add some code to change the output
// canvas/video (e.g. add effects, resize, etc.)

async function videoToCanvasToVideo(videoUrl, opts={}) {
  return new Promise(async (resolve) => {

    let videoBlob = await fetch(videoUrl).then(r => r.blob()); // <-- fully download it first (no buffering)
    let videoObjectUrl = URL.createObjectURL(videoBlob);
    let video = document.createElement("video");

    let method;

    if(document.createElement('canvas').toDataURL('image/webp').startsWith('data:image/webp')) {
      method = "whammy";
      if(!window.Whammy) await new Promise((resolve, reject) => {let js = document.createElement("script"); js.src="/library/whammy.js"; js.onload=resolve; js.onerror=reject; document.body.appendChild(js)});
    } else if(window.MediaRecorder && document.createElement('canvas').captureStream) {
      method = "mediarecorder";
    } else {
      method = "libwebp";
      if(!window.WebPEncoder) await new Promise((resolve, reject) => {let js = document.createElement("script"); js.src="/library/libwebp-0.1.3.min.js"; js.onload=resolve; js.onerror=reject; document.body.appendChild(js)});
    }

    video.addEventListener('loadeddata', async function() {
      let canvas = document.createElement('canvas');
      let context = canvas.getContext('2d');
      let [w, h] = [video.videoWidth, video.videoHeight]
      canvas.width = w;
      canvas.height = h;


      if(method === "whammy" || method === "libwebp") {
        
        let seekResolve;
        video.addEventListener('seeked', async function() {
          if(seekResolve) seekResolve();
        });
        
        let frames = [];
        let interval = 1 / opts.fps;
        let currentTime = 0;

        // workaround chromium metadata bug:
        while(video.duration === Infinity) {
          await new Promise(r => setTimeout(r, 1000));
          video.currentTime = 10000000*Math.random();
        }
        let duration = video.duration;

        let backupWebpEncoder = null;

        while(currentTime < duration) {
          video.currentTime = currentTime;
          await new Promise(r => seekResolve=r);

          context.drawImage(video, 0, 0, w, h);
          // edit frame here (add effects or whatever)
          let base64ImageData = canvas.toDataURL("image/webp");

          if(method === "whammy") {
             frames.push(base64ImageData);
          } else if(method === "libwebp") {
            let out = {output:''};
            let input = context.getImageData(0, 0, canvas.width, canvas.height);
            let [w, h] = [input.width, input.height];
            let inputData = input.data;

            if(!backupWebpEncoder) {
              backupWebpEncoder = new WebPEncoder();
              let config = {};
              config.method = 4;           // quality/speed trade-off (0=fast, 6=slower-better)
              config.sns_strength = 50;    // Spatial Noise Shaping. 0=off, 100=maximum.
              config.filter_strength = 20; // range: [0 = off .. 100 = strongest]
              config.filter_sharpness = 0; // range: [0 = off .. 7 = least sharp]
              config.filter_type = 0;			 // filtering type: 0 = simple, 1 = strong (only used if filter_strength > 0 or autofilter > 0)
              config.partitions = 0;			 // log2(number of token partitions) in [0..3] Default is set to 0 for easier progressive decoding.
              config.segments = 4;				 // maximum number of segments to use, in [1..4]
              config.pass = 1;						 // number of entropy-analysis passes (in [1..10]).
              config.show_compressed = 0;	 // if true, export the compressed picture back. In-loop filtering is not applied.
              config.preprocessing = 0;		 // preprocessing filter (0=none, 1=segment-smooth)
              config.autofilter = 0;			 // Auto adjust filter's strength [0 = off, 1 = on]
              config.partition_limit = 0;
              config.extra_info_type = 2;	 // print extra_info
              config.preset = 0; 	
              backupWebpEncoder.WebPEncodeConfig(config);
            }
            backupWebpEncoder.WebPEncodeRGBA(inputData, w, h, w*4, 92, out);
            base64ImageData = "data:image/webp;base64," + btoa(out.output);
            frames.push(base64ImageData);
          }

          currentTime += interval;
        }

        let webmEncoder = new Whammy.Video(opts.fps); 
        frames.forEach(f => webmEncoder.add(f));
        let blob = await new Promise(resolve => webmEncoder.compile(false, resolve));
        let videoBlobUrl = URL.createObjectURL(blob);
        resolve(videoBlobUrl);

      } else if(method === "mediarecorder") {

        let canvasStream = canvas.captureStream(opts.fps);
        let recorder = new MediaRecorder(canvasStream);
        let chunks = [];
        let stopRendering = false;
        video.onplay = () => {
          function step() {
            context.drawImage(video, 0, 0, w, h);
            if(opts.overlayImage) context.drawImage(opts.overlayImage, 0, 0, w, h);
            //if(!stopRendering) setTimeout(step, 1);
            if(!stopRendering) requestAnimationFrame(step);
          }
          step();
        };
        video.play();
        video.onended = function() {
          recorder.stop();
          stopRendering = true;
        }
        recorder.start();
        recorder.ondataavailable = e => chunks.push(e.data);

        recorder.onstop = function() {
          let blob = new Blob(chunks);
          let videoBlobUrl = URL.createObjectURL(blob);
          resolve(videoBlobUrl);
        }

      }


    });

    video.src = videoObjectUrl; 

  });
}
