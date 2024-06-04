<!DOCTYPE html>
<html>

<head>
  <meta charset="utf-8">
  <style>
    .message-ball {
      font-size: 20px;
      line-height: 400px;
      text-align: center;
    }
    .circle {
      transition-property: width, height;
      transition-duration: 5s;
      position: fixed;
    //  transform: translateX(-50%) translateY(-50%);
      background-color: red;
      border-radius: 50%;
    }
  </style>
</head>

<body>

  <button onclick="go()">Click me</button>

  <script>

  function go() {
    var circle=document.querySelector(".circle");
    if(!!circle)
    {
      document.body.removeChild(circle);
    }

    showCircle(200, 200, 200, div => {
      div.classList.add('message-ball');
      div.append("Hello, world!");
    });
  }

  function showCircle(cx, cy, radius, callback) {
    let div = document.createElement('div');
    div.style.width = 0;
    div.style.height = 0;
    div.style.left = cx + 'px';
    div.style.top = cy + 'px';
    div.className = 'circle';
    document.body.append(div);

    setTimeout(() => {
      div.style.width = radius * 2 + 'px';
      div.style.height = radius * 2 + 'px';

      div.addEventListener('transitionend', function handler() {
        div.removeEventListener('transitionend', handler);
        callback(div);
      });
    }, 500);
  }
  </script>


</body>
</html>
