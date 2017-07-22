var dgram = require('dgram');

sock = dgram.createSocket("udp4", function (msg, rinfo) {
  console.log('got message from '+ rinfo.address +':'+ rinfo.port);
  console.log('data len: '+ rinfo.size + " data: "+
               msg.toString('ascii', 0, rinfo.size));

});

sock.bind(22222, '192.168.144.79');
