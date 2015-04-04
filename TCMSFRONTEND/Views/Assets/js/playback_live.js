//var ff22 = document.getElementById('inpPlayback').value;
//ff22v = ff22.replace("\\", "/");
//var ff23 = document.getElementById('imgPlayback').src;
//var baseAddr = "streamer=rtmp://ptv-overonchlivefs.fplive.net/ptv-overonch04live-live/&amp;file=";
//var srcs = [
//    { "file": baseAddr + ff22.replace('stream3', 'stream1'), "label": "576p" }
//    , { "file": baseAddr + ff22.replace('stream3', 'stream2'), "label": "480p" }
//    , { "file": baseAddr + ff22, "label": "360p", "default": true }
//    , { "file": baseAddr + ff22.replace('stream3', 'stream4'), "label": "270p" }
//    , { "file": baseAddr + ff22.replace('stream3', 'stream5'), "label": "180p" }
//];
jwplayer("mediaplayer").setup({
    file: 'stream3',
    provider: "rtmp",
    streamer: "rtmp://ptv-overonchlivefs.fplive.net/ptv-overonch04live-live",
    height: '100%',
    autostart: true,
    startparam: "start",
    width: '100%',
    primary: "flash",
    skin: "/views/assets/player/six.xml",
    stretching: "fill"
});