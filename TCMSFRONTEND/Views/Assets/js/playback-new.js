var videoConf = {
   // file: 'http://107.189.40.49:1935/vod/_definst_/smil:http/ptv' + document.getElementById('inpPlayback').value.replace('.mp4', '.smil') + '/playlist.m3u8',
    //file: 'https://5a61de8ed719d.streamlock.net/vod/_definst_/smil:http/' + document.getElementById('inpPlayback').value.replace('.mp4', '.smil') + '/playlist.m3u8',
	 file: 'https://video.presstv.com/vod/_definst_/smil:path1//ptv' + document.getElementById('inpPlayback').value.replace('.mp4', '.smil') + '/playlist.m3u8',
			
	//file: 'https://video.presstv.com/vod/_definst_/smil:http/' + document.getElementById('inpPlayback').value.replace('.mp4', '.smil') + '/playlist.m3u8',
	//http://199.101.134.214:1935/vod/_definst_/smil:http/sitevideo/20180613/rome.smil/playlist.m3u8
    image: document.getElementById('imgPlayback').src
};
var conf = { autoplay: false, clip: { sources: [{ type: "application/x-mpegurl", src: videoConf.file }] } };
var player = flowplayer("#mediaplayer", conf);