var dgram = require('dgram');

sock = dgram.createSocket("udp4", function (msg, rinfo) {
  console.log('got message from '+ rinfo.address +':'+ rinfo.port);
  console.log('data len: '+ rinfo.size + " data: "+
              msg.toString('ascii', 0, rinfo.size));

  var message = new Buffer(msg.toString('ascii', 0, rinfo.size));
  sock.send(message, 0, message.length, 22222, rinfo.address);
});

sock.bind(3333, '172.20.1.29');
