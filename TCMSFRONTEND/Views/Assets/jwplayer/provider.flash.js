webpackJsonpjwplayer([2],{26:function(t,e,i){var n,a;n=[i(1),i(29),i(2),i(21),i(20)],a=function(t,e,i,n,a){function r(e){if(this._currentTextTrackIndex=-1,e){if(this._textTracks?(this._textTracks=t.reject(this._textTracks,function(t){if(this.renderNatively&&"nativecaptions"===t._id)return delete this._tracksById[t._id],!0},this),delete this._tracksById.nativemetadata):this._initTextTracks(),e.length){var n=0,r=e.length;for(n;n<r;n++){var s=e[n];if(!s._id){if("captions"===s.kind||"metadata"===s.kind){if(s._id="native"+s.kind,!s.label&&"captions"===s.kind){var c=a.createLabel(s,this._unknownCount);s.name=c.label,this._unknownCount=c.unknownCount}}else s._id=a.createId(s,this._textTracks.length);if(this._tracksById[s._id])continue;s.inuse=!0}if(s.inuse&&!this._tracksById[s._id])if("metadata"===s.kind)s.mode="hidden",s.oncuechange=B.bind(this),this._tracksById[s._id]=s;else if(p(s.kind)){var u,d=s.mode;if(s.mode="hidden",!s.cues.length&&s.embedded)continue;if(s.mode=d,this._cuesByTrackId[s._id]&&!this._cuesByTrackId[s._id].loaded){for(var o=this._cuesByTrackId[s._id].cues;u=o.shift();)E(s,u);s.mode=d,this._cuesByTrackId[s._id].loaded=!0}A.call(this,s)}}}this.renderNatively&&(this.textTrackChangeHandler=this.textTrackChangeHandler||v.bind(this),this.addTracksListener(this.video.textTracks,"change",this.textTrackChangeHandler),(i.isEdge()||i.isFF())&&(this.addTrackHandler=this.addTrackHandler||g.bind(this),this.addTracksListener(this.video.textTracks,"addtrack",this.addTrackHandler))),this._textTracks.length&&this.trigger("subtitlesTracks",{tracks:this._textTracks})}}function s(t){if(this.renderNatively){var e=t===this._itemTracks;e||n.cancelXhr(this._itemTracks),this._itemTracks=t,t&&(e||(this.disableTextTrack(),L.call(this),this.addTextTracks(t)))}}function c(){return this._currentTextTrackIndex}function u(e){this._textTracks&&(0===e&&t.each(this._textTracks,function(t){t.mode=t.embedded?"hidden":"disabled"}),this._currentTextTrackIndex!==e-1&&(this.disableTextTrack(),this._currentTextTrackIndex=e-1,this.renderNatively&&(this._textTracks[this._currentTextTrackIndex]&&(this._textTracks[this._currentTextTrackIndex].mode="showing"),this.trigger("subtitlesTrackChanged",{currentTrack:this._currentTextTrackIndex+1,tracks:this._textTracks}))))}function d(t){if(t.text&&t.begin&&t.end){var e=t.trackid.toString(),i=this._tracksById&&this._tracksById[e];i||(i={kind:"captions",_id:e,data:[]},this.addTextTracks([i]),this.trigger("subtitlesTracks",{tracks:this._textTracks}));var a;t.useDTS&&(i.source||(i.source=t.source||"mpegts")),a=t.begin+"_"+t.text;var r=this._metaCuesByTextTime[a];if(!r){r={begin:t.begin,end:t.end,text:t.text},this._metaCuesByTextTime[a]=r;var s=n.convertToVTTCues([r])[0];i.data.push(s)}}}function o(t){this._tracksById||this._initTextTracks();var e="native"+t.type,i=this._tracksById[e],n="captions"===t.type?"Unknown CC":"ID3 Metadata",a=t.cue;if(!i){var r={kind:t.type,_id:e,label:n,embedded:!0};i=I.call(this,r),this.renderNatively||"metadata"===i.kind?this.setTextTracks(this.video.textTracks):m.call(this,[i])}R.call(this,i,a)&&(this.renderNatively||"metadata"===i.kind?E(i,a):i.data.push(a))}function h(t){var e=this._tracksById[t.name];if(e){e.source=t.source;for(var i=t.captions||[],a=[],r=!1,s=0;s<i.length;s++){var c=i[s],u=t.name+"_"+c.begin+"_"+c.end;this._metaCuesByTextTime[u]||(this._metaCuesByTextTime[u]=c,a.push(c),r=!0)}r&&a.sort(function(t,e){return t.begin-e.begin});var d=n.convertToVTTCues(a);Array.prototype.push.apply(e.data,d)}}function l(t,e,i){t&&(T(t,e,i),this.instreamMode||(t.addEventListener?t.addEventListener(e,i):t["on"+e]=i))}function T(t,e,i){t&&(t.removeEventListener?t.removeEventListener(e,i):t["on"+e]=null)}function f(){n.cancelXhr(this._itemTracks);var t=this._tracksById&&this._tracksById.nativemetadata;(this.renderNatively||t)&&(C.call(this,this.video.textTracks),t&&(t.oncuechange=null)),this._itemTracks=null,this._textTracks=null,this._tracksById=null,this._cuesByTrackId=null,this._metaCuesByTextTime=null,this._unknownCount=0,this._activeCuePosition=null,this.renderNatively&&(this.removeTracksListener(this.video.textTracks,"change",this.textTrackChangeHandler),C.call(this,this.video.textTracks))}function k(){if(this._textTracks){var t=this._textTracks[this._currentTextTrackIndex];t&&(t.mode=t.embedded||"nativecaptions"===t._id?"hidden":"disabled")}}function _(){if(this._textTracks){var t=this._textTracks[this._currentTextTrackIndex];t&&(t.mode="showing")}}function v(){var e=this.video.textTracks,i=t.filter(e,function(t){return(t.inuse||!t._id)&&p(t.kind)});if(!this._textTracks||w.call(this,i))return void this.setTextTracks(e);var n=-1,a=0;for(a;a<this._textTracks.length;a++)if("showing"===this._textTracks[a].mode){n=a;break}n!==this._currentTextTrackIndex&&this.setSubtitlesTrack(n+1)}function g(){this.setTextTracks(this.video.textTracks)}function m(t){if(t){this._textTracks||this._initTextTracks();for(var e=0;e<t.length;e++){var i=t[e];if(!i.kind||p(i.kind)){var a=I.call(this,i);A.call(this,a),i.file&&(i.data=[],n.loadFile(i,this.addVTTCuesToTrack.bind(this,a),M))}}!this.renderNatively&&this._textTracks&&this._textTracks.length&&this.trigger("subtitlesTracks",{tracks:this._textTracks})}}function x(t,e){if(this.renderNatively){var i=this._tracksById[t._id];if(!i)return this._cuesByTrackId||(this._cuesByTrackId={}),void(this._cuesByTrackId[t._id]={cues:e,loaded:!1});if(!this._cuesByTrackId[t._id]||!this._cuesByTrackId[t._id].loaded){var n;for(this._cuesByTrackId[t._id]={cues:e,loaded:!0};n=e.shift();)E(i,n)}}}function y(){var e=this.video.textTracks;e&&e.length&&t.each(e,function(t){for(var e=t.cues?t.cues.length:0,i=e;i--;)t.removeCue(t.cues[i])})}function E(t,e){if(!i.isEdge()||!window.TextTrackCue)return void t.addCue(e);var n=new window.TextTrackCue(e.startTime,e.endTime,e.text);t.addCue(n)}function C(e){e.length&&t.each(e,function(t){t.mode="hidden";for(var e=t.cues.length;e--;)t.removeCue(t.cues[e]);t.embedded||(t.mode="disabled"),t.inuse=!1})}function p(t){return"subtitles"===t||"captions"===t}function b(){this._textTracks=[],this._tracksById={},this._metaCuesByTextTime={},this._cuesByTrackId={},this._cachedVTTCues={},this._unknownCount=0}function I(e){var i,n=a.createLabel(e,this._unknownCount),r=n.label;if(this._unknownCount=n.unknownCount,this.renderNatively||"metadata"===e.kind){var s=this.video.textTracks;i=t.findWhere(s,{label:r}),i?(i.kind=e.kind,i.language=e.language||""):i=this.video.addTextTrack(e.kind,r,e.language||""),i["default"]=e["default"],i.mode="disabled",i.inuse=!0}else i=e,i.data=i.data||[];return i._id||(i._id=a.createId(e,this._textTracks.length)),i}function A(t){this._textTracks.push(t),this._tracksById[t._id]=t}function L(){if(this._textTracks){var e=t.filter(this._textTracks,function(t){return t.embedded||"subs"===t.groupid});this._initTextTracks(),t.each(e,function(t){this._tracksById[t._id]=t}),this._textTracks=e}}function B(i){var n=i.currentTarget.activeCues;if(n&&n.length){var a=n[n.length-1].startTime;if(this._activeCuePosition!==a){var r=[];if(t.each(n,function(t){t.startTime<a||(t.data||t.value?r.push(t):t.text&&this.trigger("meta",{metadataTime:a,metadata:JSON.parse(t.text)}))},this),r.length){var s=e.parseID3(r);this.trigger("meta",{metadataTime:a,metadata:s})}this._activeCuePosition=a}}}function R(t,e){var i=t.kind;this._cachedVTTCues[t._id]||(this._cachedVTTCues[t._id]={});var n,a=this._cachedVTTCues[t._id];switch(i){case"captions":n=Math.floor(20*e.startTime);var r=Math.floor(20*e.endTime),s=a[n]||a[n+1]||a[n-1];return!(s&&Math.abs(s-r)<=1)&&(a[n]=r,!0);case"metadata":var c=e.data?new Uint8Array(e.data).join(""):e.text;return n=e.startTime+c,!a[n]&&(a[n]=e.endTime,!0)}}function w(t){if(t.length>this._textTracks.length)return!0;for(var e=0;e<t.length;e++){var i=t[e];if(!i._id||!this._tracksById[i._id])return!0}return!1}function M(t){i.log("CAPTIONS("+t+")")}var P={_itemTracks:null,_textTracks:null,_tracksById:null,_cuesByTrackId:null,_cachedVTTCues:null,_metaCuesByTextTime:null,_currentTextTrackIndex:-1,_unknownCount:0,_activeCuePosition:null,_initTextTracks:b,addTracksListener:l,clearCues:y,clearTracks:f,disableTextTrack:k,enableTextTrack:_,getSubtitlesTrack:c,removeTracksListener:T,addTextTracks:m,setTextTracks:r,setupSideloadedTracks:s,setSubtitlesTrack:u,textTrackChangeHandler:null,addTrackHandler:null,addCuesToTrack:h,addCaptionsCue:d,addVTTCue:o,addVTTCuesToTrack:x,renderNatively:!1};return P}.apply(e,n),!(void 0!==a&&(t.exports=a))},48:function(t,e,i){var n,a;n=[i(2),i(1),i(4),i(6),i(53),i(16),i(3),i(26)],a=function(t,e,i,n,a,r,s,c){function u(t){return t+"_swf_"+h++}function d(e){var i=document.createElement("a");i.href=e.flashplayer;var n=i.hostname===window.location.host;return t.isChrome()&&!n}function o(t,o){function h(t){var e=D[t];if(!e){for(var i=1/0,n=D.bitrates.length;n--;){var a=Math.abs(D.bitrates[n]-t);if(a>i)break;i=a}e=D.labels[D.bitrates[n+1]],D[t]=e}return e}function l(){var t=o.hlslabels;if(!t)return null;var e={},i=[];for(var n in t){var a=parseFloat(n);if(!isNaN(a)){var r=Math.round(a);e[r]=t[n],i.push(r)}}return 0===i.length?null:(i.sort(function(t,e){return t-e}),{labels:e,bitrates:i})}function T(){C=setTimeout(function(){s.trigger.call(w,"flashBlocked")},4e3),x.once("embedded",function(){k(),s.trigger.call(w,"flashUnblocked")},w)}function f(){k(),T()}function k(){clearTimeout(C),window.removeEventListener("focus",f)}function _(t){for(var e=t.levels,i=0;i<e.length;i++){var n=e[i];if(n.index=i,D&&n.bitrate){var a=Math.round(n.bitrate/1e3);n.label=h(a)}}t.levels=I=v(t.levels),t.currentQuality=b=g(I,t.currentQuality)}function v(t){return t.sort(function(t,e){return t.height&&e.height?e.height-t.height:e.bitrate-t.bitrate})}function g(t,e){for(var i=0;i<t.length;i++)if(t[i].index===e)return i}var m,x,y,E=null,C=-1,p=!1,b=-1,I=null,A=-1,L=null,B=!0,R=!1,w=this,M=function(){return x&&x.__ready},P=function(){x&&x.triggerFlash.apply(x,arguments)},D=l();e.extend(this,s,c,{init:function(t){t.preload&&"none"!==t.preload&&!o.autostart&&(E=t)},load:function(t){E=t,p=!1,this.setState(n.LOADING),P("load",t),t.sources.length&&"hls"!==t.sources[0].type&&this.sendMediaType(t.sources)},play:function(){P("play")},pause:function(){P("pause"),this.setState(n.PAUSED)},stop:function(){P("stop"),b=-1,E=null,this.clearTracks(),this.setState(n.IDLE)},seek:function(t){P("seek",t)},volume:function(t){if(e.isNumber(t)){var i=Math.min(Math.max(0,t),100);M()&&P("volume",i)}},mute:function(t){M()&&P("mute",t)},setState:function(){return r.setState.apply(this,arguments)},checkComplete:function(){return p},attachMedia:function(){B=!0,p&&(this.setState(n.COMPLETE),this.trigger(i.JWPLAYER_MEDIA_COMPLETE),p=!1)},detachMedia:function(){return B=!1,null},getSwfObject:function(e){var i=e.getElementsByTagName("object")[0];return i?(i.off(null,null,this),i):a.embed(o.flashplayer,e,u(t),o.wmode)},getContainer:function(){return m},setContainer:function(t){if(m!==t){m=t,x=this.getSwfObject(t),document.hasFocus()?T():window.addEventListener("focus",f),x.once("ready",function(){k(),x.once("pluginsLoaded",function(){x.queueCommands=!1,P("setupCommandQueue",x.__commandQueue),x.__commandQueue=[]});var t=e.extend({},o),n=x.triggerFlash("setup",t);n===x?x.__ready=!0:this.trigger(i.JWPLAYER_MEDIA_ERROR,n),E&&P("init",E)},this);var a=[i.JWPLAYER_MEDIA_ERROR,i.JWPLAYER_MEDIA_SEEK,i.JWPLAYER_MEDIA_SEEKED,"subtitlesTrackChanged","mediaType"],r=[i.JWPLAYER_MEDIA_BUFFER,i.JWPLAYER_MEDIA_TIME],c=[i.JWPLAYER_MEDIA_BUFFER_FULL];x.on([i.JWPLAYER_MEDIA_LEVELS,i.JWPLAYER_MEDIA_LEVEL_CHANGED].join(" "),function(t){_(t),this.trigger(t.type,t)},this),x.on(i.JWPLAYER_AUDIO_TRACKS,function(t){A=t.currentTrack,L=t.tracks,this.trigger(t.type,t)},this),x.on(i.JWPLAYER_AUDIO_TRACK_CHANGED,function(t){A=t.currentTrack,L=t.tracks,this.trigger(t.type,t)},this),x.on(i.JWPLAYER_PLAYER_STATE,function(t){var e=t.newstate;e!==n.IDLE&&this.setState(e)},this),x.on(r.join(" "),function(t){"Infinity"===t.duration&&(t.duration=1/0),this.trigger(t.type,t)},this),x.on(a.join(" "),function(t){this.trigger(t.type,t)},this),x.on(c.join(" "),function(t){this.trigger(t.type)},this),x.on(i.JWPLAYER_MEDIA_BEFORECOMPLETE,function(t){p=!0,this.trigger(t.type),B===!0&&(p=!1)},this),x.on(i.JWPLAYER_MEDIA_COMPLETE,function(t){p||(this.setState(n.COMPLETE),this.trigger(t.type))},this),x.on("visualQuality",function(t){var i=0;I.length>1&&(i=g(I,t.level.index+1)),t.level=e.extend(t.level,{index:i}),t.reason=t.reason||"api",this.trigger("visualQuality",t),this.trigger("providerFirstFrame",{})},this),x.on(i.JWPLAYER_PROVIDER_CHANGED,function(t){y=t.message,this.trigger(i.JWPLAYER_PROVIDER_CHANGED,t)},this),x.on(i.JWPLAYER_ERROR,function(t){this.trigger(i.JWPLAYER_MEDIA_ERROR,t)},this),x.on("subtitlesTracks",function(t){this.addTextTracks(t.tracks)},this),x.on("subtitlesTrackData",function(t){this.addCuesToTrack(t)},this),x.on(i.JWPLAYER_MEDIA_META,function(t){t.metadata&&"textdata"===t.metadata.type?this.addCaptionsCue(t.metadata):this.trigger(t.type,t)},this),d(o)&&x.on("throttle",function(t){k(),"resume"===t.state?s.trigger.call(w,"flashThrottle",t):C=setTimeout(function(){s.trigger.call(w,"flashThrottle",t)},250)},this)}},remove:function(){b=-1,I=null,a.remove(x)},setVisibility:function(t){t=!!t,m.style.opacity=t?1:0},resize:function(t,e,i){i&&P("stretch",i)},setControls:function(t){P("setControls",t)},setFullscreen:function(t){R=t,P("fullscreen",t)},getFullScreen:function(){return R},setCurrentQuality:function(t){P("setCurrentQuality",I[t].index)},getCurrentQuality:function(){return b},setSubtitlesTrack:function(t){P("setSubtitlesTrack",t)},getName:function(){return y?{name:"flash_"+y}:{name:"flash"}},getQualityLevels:function(){return I||E&&E.sources},getAudioTracks:function(){return L},getCurrentAudioTrack:function(){return A},setCurrentAudioTrack:function(t){P("setCurrentAudioTrack",t)},destroy:function(){k(),this.remove(),x&&(x.off(),x=null),m=null,E=null,this.off()}}),this.trigger=function(t,e){if(B)return s.trigger.call(this,t,e)}}var h=0,l=function(){};return l.prototype=r,o.prototype=new l,o.getName=function(){return{name:"flash"}},o}.apply(e,n),!(void 0!==a&&(t.exports=a))}});
//# sourceMappingURL=provider.flash.e86535d11f2f8dbcf3d5.map