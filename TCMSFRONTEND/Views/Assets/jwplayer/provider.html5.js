webpackJsonpjwplayer([1],{26:function(t,e,i){var a,r;a=[i(1),i(29),i(2),i(21),i(20)],r=function(t,e,i,a,r){function n(e){if(this._currentTextTrackIndex=-1,e){if(this._textTracks?(this._textTracks=t.reject(this._textTracks,function(t){if(this.renderNatively&&"nativecaptions"===t._id)return delete this._tracksById[t._id],!0},this),delete this._tracksById.nativemetadata):this._initTextTracks(),e.length){var a=0,n=e.length;for(a;a<n;a++){var s=e[a];if(!s._id){if("captions"===s.kind||"metadata"===s.kind){if(s._id="native"+s.kind,!s.label&&"captions"===s.kind){var c=r.createLabel(s,this._unknownCount);s.name=c.label,this._unknownCount=c.unknownCount}}else s._id=r.createId(s,this._textTracks.length);if(this._tracksById[s._id])continue;s.inuse=!0}if(s.inuse&&!this._tracksById[s._id])if("metadata"===s.kind)s.mode="hidden",s.oncuechange=L.bind(this),this._tracksById[s._id]=s;else if(E(s.kind)){var d,u=s.mode;if(s.mode="hidden",!s.cues.length&&s.embedded)continue;if(s.mode=u,this._cuesByTrackId[s._id]&&!this._cuesByTrackId[s._id].loaded){for(var o=this._cuesByTrackId[s._id].cues;d=o.shift();)p(s,d);s.mode=u,this._cuesByTrackId[s._id].loaded=!0}w.call(this,s)}}}this.renderNatively&&(this.textTrackChangeHandler=this.textTrackChangeHandler||v.bind(this),this.addTracksListener(this.video.textTracks,"change",this.textTrackChangeHandler),(i.isEdge()||i.isFF())&&(this.addTrackHandler=this.addTrackHandler||_.bind(this),this.addTracksListener(this.video.textTracks,"addtrack",this.addTrackHandler))),this._textTracks.length&&this.trigger("subtitlesTracks",{tracks:this._textTracks})}}function s(t){if(this.renderNatively){var e=t===this._itemTracks;e||a.cancelXhr(this._itemTracks),this._itemTracks=t,t&&(e||(this.disableTextTrack(),A.call(this),this.addTextTracks(t)))}}function c(){return this._currentTextTrackIndex}function d(e){this._textTracks&&(0===e&&t.each(this._textTracks,function(t){t.mode=t.embedded?"hidden":"disabled"}),this._currentTextTrackIndex!==e-1&&(this.disableTextTrack(),this._currentTextTrackIndex=e-1,this.renderNatively&&(this._textTracks[this._currentTextTrackIndex]&&(this._textTracks[this._currentTextTrackIndex].mode="showing"),this.trigger("subtitlesTrackChanged",{currentTrack:this._currentTextTrackIndex+1,tracks:this._textTracks}))))}function u(t){if(t.text&&t.begin&&t.end){var e=t.trackid.toString(),i=this._tracksById&&this._tracksById[e];i||(i={kind:"captions",_id:e,data:[]},this.addTextTracks([i]),this.trigger("subtitlesTracks",{tracks:this._textTracks}));var r;t.useDTS&&(i.source||(i.source=t.source||"mpegts")),r=t.begin+"_"+t.text;var n=this._metaCuesByTextTime[r];if(!n){n={begin:t.begin,end:t.end,text:t.text},this._metaCuesByTextTime[r]=n;var s=a.convertToVTTCues([n])[0];i.data.push(s)}}}function o(t){this._tracksById||this._initTextTracks();var e="native"+t.type,i=this._tracksById[e],a="captions"===t.type?"Unknown CC":"ID3 Metadata",r=t.cue;if(!i){var n={kind:t.type,_id:e,label:a,embedded:!0};i=C.call(this,n),this.renderNatively||"metadata"===i.kind?this.setTextTracks(this.video.textTracks):m.call(this,[i])}S.call(this,i,r)&&(this.renderNatively||"metadata"===i.kind?p(i,r):i.data.push(r))}function h(t){var e=this._tracksById[t.name];if(e){e.source=t.source;for(var i=t.captions||[],r=[],n=!1,s=0;s<i.length;s++){var c=i[s],d=t.name+"_"+c.begin+"_"+c.end;this._metaCuesByTextTime[d]||(this._metaCuesByTextTime[d]=c,r.push(c),n=!0)}n&&r.sort(function(t,e){return t.begin-e.begin});var u=a.convertToVTTCues(r);Array.prototype.push.apply(e.data,u)}}function l(t,e,i){t&&(T(t,e,i),this.instreamMode||(t.addEventListener?t.addEventListener(e,i):t["on"+e]=i))}function T(t,e,i){t&&(t.removeEventListener?t.removeEventListener(e,i):t["on"+e]=null)}function k(){a.cancelXhr(this._itemTracks);var t=this._tracksById&&this._tracksById.nativemetadata;(this.renderNatively||t)&&(b.call(this,this.video.textTracks),t&&(t.oncuechange=null)),this._itemTracks=null,this._textTracks=null,this._tracksById=null,this._cuesByTrackId=null,this._metaCuesByTextTime=null,this._unknownCount=0,this._activeCuePosition=null,this.renderNatively&&(this.removeTracksListener(this.video.textTracks,"change",this.textTrackChangeHandler),b.call(this,this.video.textTracks))}function f(){if(this._textTracks){var t=this._textTracks[this._currentTextTrackIndex];t&&(t.mode=t.embedded||"nativecaptions"===t._id?"hidden":"disabled")}}function g(){if(this._textTracks){var t=this._textTracks[this._currentTextTrackIndex];t&&(t.mode="showing")}}function v(){var e=this.video.textTracks,i=t.filter(e,function(t){return(t.inuse||!t._id)&&E(t.kind)});if(!this._textTracks||B.call(this,i))return void this.setTextTracks(e);var a=-1,r=0;for(r;r<this._textTracks.length;r++)if("showing"===this._textTracks[r].mode){a=r;break}a!==this._currentTextTrackIndex&&this.setSubtitlesTrack(a+1)}function _(){this.setTextTracks(this.video.textTracks)}function m(t){if(t){this._textTracks||this._initTextTracks();for(var e=0;e<t.length;e++){var i=t[e];if(!i.kind||E(i.kind)){var r=C.call(this,i);w.call(this,r),i.file&&(i.data=[],a.loadFile(i,this.addVTTCuesToTrack.bind(this,r),M))}}!this.renderNatively&&this._textTracks&&this._textTracks.length&&this.trigger("subtitlesTracks",{tracks:this._textTracks})}}function x(t,e){if(this.renderNatively){var i=this._tracksById[t._id];if(!i)return this._cuesByTrackId||(this._cuesByTrackId={}),void(this._cuesByTrackId[t._id]={cues:e,loaded:!1});if(!this._cuesByTrackId[t._id]||!this._cuesByTrackId[t._id].loaded){var a;for(this._cuesByTrackId[t._id]={cues:e,loaded:!0};a=e.shift();)p(i,a)}}}function y(){var e=this.video.textTracks;e&&e.length&&t.each(e,function(t){for(var e=t.cues?t.cues.length:0,i=e;i--;)t.removeCue(t.cues[i])})}function p(t,e){if(!i.isEdge()||!window.TextTrackCue)return void t.addCue(e);var a=new window.TextTrackCue(e.startTime,e.endTime,e.text);t.addCue(a)}function b(e){e.length&&t.each(e,function(t){t.mode="hidden";for(var e=t.cues.length;e--;)t.removeCue(t.cues[e]);t.embedded||(t.mode="disabled"),t.inuse=!1})}function E(t){return"subtitles"===t||"captions"===t}function I(){this._textTracks=[],this._tracksById={},this._metaCuesByTextTime={},this._cuesByTrackId={},this._cachedVTTCues={},this._unknownCount=0}function C(e){var i,a=r.createLabel(e,this._unknownCount),n=a.label;if(this._unknownCount=a.unknownCount,this.renderNatively||"metadata"===e.kind){var s=this.video.textTracks;i=t.findWhere(s,{label:n}),i?(i.kind=e.kind,i.language=e.language||""):i=this.video.addTextTrack(e.kind,n,e.language||""),i["default"]=e["default"],i.mode="disabled",i.inuse=!0}else i=e,i.data=i.data||[];return i._id||(i._id=r.createId(e,this._textTracks.length)),i}function w(t){this._textTracks.push(t),this._tracksById[t._id]=t}function A(){if(this._textTracks){var e=t.filter(this._textTracks,function(t){return t.embedded||"subs"===t.groupid});this._initTextTracks(),t.each(e,function(t){this._tracksById[t._id]=t}),this._textTracks=e}}function L(i){var a=i.currentTarget.activeCues;if(a&&a.length){var r=a[a.length-1].startTime;if(this._activeCuePosition!==r){var n=[];if(t.each(a,function(t){t.startTime<r||(t.data||t.value?n.push(t):t.text&&this.trigger("meta",{metadataTime:r,metadata:JSON.parse(t.text)}))},this),n.length){var s=e.parseID3(n);this.trigger("meta",{metadataTime:r,metadata:s})}this._activeCuePosition=r}}}function S(t,e){var i=t.kind;this._cachedVTTCues[t._id]||(this._cachedVTTCues[t._id]={});var a,r=this._cachedVTTCues[t._id];switch(i){case"captions":a=Math.floor(20*e.startTime);var n=Math.floor(20*e.endTime),s=r[a]||r[a+1]||r[a-1];return!(s&&Math.abs(s-n)<=1)&&(r[a]=n,!0);case"metadata":var c=e.data?new Uint8Array(e.data).join(""):e.text;return a=e.startTime+c,!r[a]&&(r[a]=e.endTime,!0)}}function B(t){if(t.length>this._textTracks.length)return!0;for(var e=0;e<t.length;e++){var i=t[e];if(!i._id||!this._tracksById[i._id])return!0}return!1}function M(t){i.log("CAPTIONS("+t+")")}var D={_itemTracks:null,_textTracks:null,_tracksById:null,_cuesByTrackId:null,_cachedVTTCues:null,_metaCuesByTextTime:null,_currentTextTrackIndex:-1,_unknownCount:0,_activeCuePosition:null,_initTextTracks:I,addTracksListener:l,clearCues:y,clearTracks:k,disableTextTrack:f,enableTextTrack:g,getSubtitlesTrack:c,removeTracksListener:T,addTextTracks:m,setTextTracks:n,setupSideloadedTracks:s,setSubtitlesTrack:d,textTrackChangeHandler:null,addTrackHandler:null,addCuesToTrack:h,addCaptionsCue:u,addVTTCue:o,addVTTCuesToTrack:x,renderNatively:!1};return D}.apply(e,a),!(void 0!==r&&(t.exports=r))},50:function(t,e,i){var a,r;a=[i(49),i(17),i(2),i(28),i(1),i(4),i(6),i(16),i(3),i(26),i(27)],r=function(t,e,i,a,r,n,s,c,d,u){function o(t,e){i.foreach(t,function(t,i){e.addEventListener(t,i,!1)})}function h(t,e){i.foreach(t,function(t,i){e.removeEventListener(t,i,!1)})}function l(l,I){function C(t,e){Ht.setAttribute(t,e||"")}function w(){wt&&(ht(Ht.audioTracks),yt.setTextTracks(Ht.textTracks),C("jw-loaded","data"))}function A(){wt&&C("jw-loaded","started")}function L(t){yt.trigger("click",t)}function S(){wt&&!Lt&&(R(F()),P(rt(),_t,vt))}function B(){wt&&P(rt(),_t,vt)}function M(){T(It),bt=!0,wt&&(yt.state===s.STALLED?yt.setState(s.PLAYING):yt.state===s.PLAYING&&(It=setTimeout(at,k)),Lt&&Ht.duration===1/0&&0===Ht.currentTime||(R(F()),N(Ht.currentTime),P(rt(),_t,vt),yt.state===s.PLAYING&&(yt.trigger(n.JWPLAYER_MEDIA_TIME,{position:_t,duration:vt}),D())))}function D(){var t=Rt.level;if(t.width!==Ht.videoWidth||t.height!==Ht.videoHeight){if(t.width=Ht.videoWidth,t.height=Ht.videoHeight,ft(),!t.width||!t.height||At===-1)return;Rt.reason=Rt.reason||"auto",Rt.mode="hls"===xt[At].type?"auto":"manual",Rt.bitrate=0,t.index=At,t.label=xt[At].label,yt.trigger("visualQuality",Rt),Rt.reason=""}}function P(t,e,i){0===i||t===Ct&&i===vt||(Ct=t,yt.trigger(n.JWPLAYER_MEDIA_BUFFER,{bufferPercent:100*t,position:e,duration:i}))}function N(t){vt<0&&(t=-(tt()-t)),_t=t}function F(){var t=Ht.duration,e=tt();if(t===1/0&&e){var i=e-$();i!==1/0&&i>f&&(t=-i)}return t}function R(t){vt=t,Et&&t&&t!==1/0&&yt.seek(Et)}function j(){var t=F();Lt&&t===1/0&&(t=0),yt.trigger(n.JWPLAYER_MEDIA_META,{duration:t,height:Ht.videoHeight,width:Ht.videoWidth}),R(t)}function O(){wt&&(bt=jt=!0,Lt||ft(),v&&yt.setTextTracks(yt._textTracks),W())}function H(){wt&&(C("jw-loaded","meta"),j())}function W(){mt||i.isIOS()&&!jt||(mt=!0,jt=!1,yt.trigger(n.JWPLAYER_MEDIA_BUFFER_FULL))}function Y(){yt.setState(s.PLAYING),Ht.hasAttribute("jw-played")||C("jw-played",""),Ht.hasAttribute("jw-gesture-required")&&Ht.removeAttribute("jw-gesture-required"),yt.trigger(n.JWPLAYER_PROVIDER_FIRST_FRAME,{})}function J(){yt.state!==s.COMPLETE&&Ht.hasAttribute("jw-played")&&Ht.currentTime!==Ht.duration&&yt.setState(s.PAUSED)}function V(){if(!(Lt||Ht.paused||Ht.ended||yt.state===s.LOADING||yt.state===s.ERROR||yt.seeking))return i.isIOS()&&Ht.duration-Ht.currentTime<=.1?void nt():void yt.setState(s.STALLED)}function G(){wt&&yt.trigger(n.JWPLAYER_MEDIA_ERROR,{message:"Error loading media: File could not be played"})}function q(t){var e;return"array"===i.typeOf(t)&&t.length>0&&(e=r.map(t,function(t,e){return{label:t.label||e}})),e}function U(t){xt=t,At=Q(t);var e=q(t);e&&yt.trigger(n.JWPLAYER_MEDIA_LEVELS,{levels:e,currentQuality:At})}function Q(t){var e=Math.max(0,At),i=I.qualityLabel;if(t)for(var a=0;a<t.length;a++)if(t[a]["default"]&&(e=a),i&&t[a].label===i)return a;return Rt.reason="initial choice",Rt.level={},e}function K(){var t=Ht.play();t&&t["catch"]?t["catch"](function(t){console.warn(t),"NotAllowedError"===t.name&&Ht.hasAttribute("jw-gesture-required")&&yt.trigger("autoplayFailed")}):Ht.hasAttribute("jw-gesture-required")&&yt.trigger("autoplayFailed")}function X(t,e){Et=0,T(It);var a=document.createElement("source");a.src=xt[At].file;var r=Ht.src!==a.src,n=Ht.getAttribute("jw-loaded"),c=Ht.hasAttribute("jw-played");r||"none"===n||"started"===n?(vt=e,z(xt[At]),yt.setupSideloadedTracks(yt._itemTracks),Ht.load()):(0===t&&Ht.currentTime>0&&(Et=-1,yt.seek(t)),K()),_t=Ht.currentTime,m&&!c&&(W(),Ht.paused||yt.state===s.PLAYING||yt.setState(s.LOADING)),i.isIOS()&&yt.getFullScreen()&&(Ht.controls=!0),t>0&&yt.seek(t)}function z(e){Pt=null,Nt=-1,Ft=-1,Rt.reason||(Rt.reason="initial choice",Rt.level={}),bt=!1,mt=!1,Lt=t(e),e.preload&&e.preload!==Ht.getAttribute("preload")&&C("preload",e.preload);var i=document.createElement("source");i.src=e.file;var a=Ht.src!==i.src;a&&(C("jw-loaded","none"),Ht.src=e.file)}function Z(){Ht&&(yt.disableTextTrack(),Ht.removeAttribute("preload"),Ht.removeAttribute("src"),Ht.removeAttribute("jw-loaded"),Ht.removeAttribute("jw-played"),a.emptyElement(Ht),At=-1,!_&&"load"in Ht&&Ht.load())}function $(){for(var t=Ht.seekable?Ht.seekable.length:0,e=1/0;t--;)e=Math.min(e,Ht.seekable.start(t));return e}function tt(){for(var t=Ht.seekable?Ht.seekable.length:0,e=0;t--;)e=Math.max(e,Ht.seekable.end(t));return e}function et(){yt.seeking=!1,yt.trigger(n.JWPLAYER_MEDIA_SEEKED)}function it(){yt.trigger("volume",{volume:Math.round(100*Ht.volume)}),yt.trigger("mute",{mute:Ht.muted})}function at(){Ht.currentTime===_t&&V()}function rt(){var t=Ht.buffered,e=Ht.duration;return!t||0===t.length||e<=0||e===1/0?0:i.between(t.end(t.length-1)/e,0,1)}function nt(){if(wt&&yt.state!==s.IDLE&&yt.state!==s.COMPLETE){if(T(It),At=-1,Bt=!0,yt.trigger(n.JWPLAYER_MEDIA_BEFORECOMPLETE),!wt)return;st()}}function st(){T(It),yt.setState(s.COMPLETE),Bt=!1,yt.trigger(n.JWPLAYER_MEDIA_COMPLETE)}function ct(t){Mt=!0,ot(t),i.isIOS()&&(Ht.controls=!1)}function dt(){for(var t=-1,e=0;e<Ht.audioTracks.length;e++)if(Ht.audioTracks[e].enabled){t=e;break}lt(t)}function ut(t){Mt=!1,ot(t),i.isIOS()&&(Ht.controls=!1)}function ot(t){yt.trigger("fullscreenchange",{target:t.target,jwstate:Mt})}function ht(t){if(Pt=null,t){if(t.length){for(var e=0;e<t.length;e++)if(t[e].enabled){Nt=e;break}Nt===-1&&(Nt=0,t[Nt].enabled=!0),Pt=r.map(t,function(t){var e={name:t.label||t.language,language:t.language};return e})}yt.addTracksListener(t,"change",dt),Pt&&yt.trigger("audioTracks",{currentTrack:Nt,tracks:Pt})}}function lt(t){Ht&&Ht.audioTracks&&Pt&&t>-1&&t<Ht.audioTracks.length&&t!==Nt&&(Ht.audioTracks[Nt].enabled=!1,Nt=t,Ht.audioTracks[Nt].enabled=!0,yt.trigger("audioTrackChanged",{currentTrack:Nt,tracks:Pt}))}function Tt(){return Pt||[]}function kt(){return Nt}function ft(){if("hls"===xt[0].type){var t="video";0===Ht.videoHeight&&(t="audio"),yt.trigger("mediaType",{mediaType:t})}}this.state=s.IDLE,this.seeking=!1,r.extend(this,d,u),this.renderNatively=i.isChrome()||i.isIOS()||i.isSafari()||i.isEdge(),this.trigger=function(t,e){if(wt)return d.trigger.call(this,t,e)},this.setState=function(t){return c.setState.call(this,t)};var gt,vt,_t,mt,xt,yt=this,pt={click:L,durationchange:S,ended:nt,error:G,loadstart:A,loadeddata:w,loadedmetadata:H,canplay:O,playing:Y,progress:B,pause:J,seeked:et,timeupdate:M,volumechange:it,webkitbeginfullscreen:ct,webkitendfullscreen:ut},bt=!1,Et=0,It=-1,Ct=-1,wt=!0,At=-1,Lt=null,St=!!I.sdkplatform,Bt=!1,Mt=!1,Dt=i.noop,Pt=null,Nt=-1,Ft=-1,Rt={level:{}},jt=!1,Ot=document.getElementById(l),Ht=Ot?Ot.querySelector("video"):void 0;Ht||(Ht=document.createElement("video"),m&&C("jw-gesture-required")),Ht.className="jw-video jw-reset",this.isSDK=St,this.video=Ht,r.isObject(I.cast)&&I.cast.appid&&C("disableRemotePlayback",""),o(pt,Ht),C("x-webkit-airplay","allow"),C("webkit-playsinline"),C("playsinline"),this.stop=function(){T(It),wt&&(Z(),this.clearTracks(),i.isIE()&&Ht.pause(),this.setState(s.IDLE))},this.destroy=function(){Dt=i.noop,h(pt,Ht),this.removeTracksListener(Ht.audioTracks,"change",dt),this.removeTracksListener(Ht.textTracks,"change",yt.textTrackChangeHandler),this.remove(),this.off()},this.init=function(t){wt&&(xt=t.sources,At=Q(t.sources),t.sources.length&&"hls"!==t.sources[0].type&&this.sendMediaType(t.sources),_t=t.starttime||0,vt=t.duration||0,Rt.reason="",z(xt[At]),this.setupSideloadedTracks(t.tracks))},this.load=function(t){wt&&(U(t.sources),t.sources.length&&"hls"!==t.sources[0].type&&this.sendMediaType(t.sources),m&&!Ht.hasAttribute("jw-played")||yt.setState(s.LOADING),X(t.starttime||0,t.duration||0))},this.play=function(){return yt.seeking?(yt.setState(s.LOADING),void yt.once(n.JWPLAYER_MEDIA_SEEKED,yt.play)):(Dt(),void K())},this.pause=function(){T(It),Ht.pause(),Dt=function(){var t=Ht.paused&&Ht.currentTime;if(t&&Ht.duration===1/0){var e=tt(),i=e-$(),a=i<f,r=e-Ht.currentTime;a&&e&&(r>15||r<0)&&(Ht.currentTime=Math.max(e-10,e-i))}},this.setState(s.PAUSED)},this.seek=function(t){if(wt)if(t<0&&(t+=$()+tt()),0===Et&&this.trigger(n.JWPLAYER_MEDIA_SEEK,{position:Ht.currentTime,offset:t}),bt||(bt=!!tt()),bt){Et=0;try{yt.seeking=!0,Ht.currentTime=t}catch(e){yt.seeking=!1,Et=t}}else Et=t,x&&Ht.paused&&K()},this.volume=function(t){t=i.between(t/100,0,1),Ht.volume=t},this.mute=function(t){Ht.muted=!!t},this.checkComplete=function(){return Bt},this.detachMedia=function(){return T(It),this.removeTracksListener(Ht.textTracks,"change",this.textTrackChangeHandler),this.disableTextTrack(),wt=!1,Ht},this.attachMedia=function(){wt=!0,bt=!1,this.seeking=!1,Ht.loop=!1,this.enableTextTrack(),this.addTracksListener(Ht.textTracks,"change",this.textTrackChangeHandler),Bt&&st()},this.setContainer=function(t){gt=t,t.appendChild(Ht)},this.getContainer=function(){return gt},this.remove=function(){Z(),T(It),gt===Ht.parentNode&&gt.removeChild(Ht)},this.setVisibility=function(t){t=!!t,t||y?e.style(gt,{visibility:"visible",opacity:1}):e.style(gt,{visibility:"",opacity:0})},this.resize=function(t,i,a){if(!(t&&i&&Ht.videoWidth&&Ht.videoHeight))return!1;var r={objectFit:"",width:"",height:""};if("uniform"===a){var n=t/i,s=Ht.videoWidth/Ht.videoHeight;Math.abs(n-s)<.09&&(r.objectFit="fill",a="exactfit")}var c=g||y||p||b;if(c){var d=-Math.floor(Ht.videoWidth/2+1),u=-Math.floor(Ht.videoHeight/2+1),o=Math.ceil(100*t/Ht.videoWidth)/100,h=Math.ceil(100*i/Ht.videoHeight)/100;"none"===a?o=h=1:"fill"===a?o=h=Math.max(o,h):"uniform"===a&&(o=h=Math.min(o,h)),r.width=Ht.videoWidth,r.height=Ht.videoHeight,r.top=r.left="50%",r.margin=0,e.transform(Ht,"translate("+d+"px, "+u+"px) scale("+o.toFixed(2)+", "+h.toFixed(2)+")")}return e.style(Ht,r),!1},this.setFullscreen=function(t){if(t=!!t){var e=i.tryCatch(function(){var t=Ht.webkitEnterFullscreen||Ht.webkitEnterFullScreen;t&&t.apply(Ht)});return!(e instanceof i.Error)&&yt.getFullScreen()}var a=Ht.webkitExitFullscreen||Ht.webkitExitFullScreen;return a&&a.apply(Ht),t},yt.getFullScreen=function(){return Mt||!!Ht.webkitDisplayingFullscreen},this.setCurrentQuality=function(t){if(At!==t&&t>=0&&xt&&xt.length>t){At=t,Rt.reason="api",Rt.level={},this.trigger(n.JWPLAYER_MEDIA_LEVEL_CHANGED,{currentQuality:t,levels:q(xt)}),I.qualityLabel=xt[t].label;var e=Ht.currentTime||0,i=Ht.duration||0;i<=0&&(i=vt),yt.setState(s.LOADING),X(e,i)}},this.getCurrentQuality=function(){return At},this.getQualityLevels=function(){return q(xt)},this.getName=function(){return{name:E}},this.setCurrentAudioTrack=lt,this.getAudioTracks=Tt,this.getCurrentAudioTrack=kt}var T=window.clearTimeout,k=256,f=120,g=i.isIE(),v=i.isIE(9),_=i.isMSIE(),m=i.isMobile(),x=i.isFF(),y=i.isAndroidNative(),p=i.isIOS(7),b=i.isIOS(8),E="html5",I=function(){};return I.prototype=c,l.prototype=new I,l.getName=function(){return{name:"html5"}},l}.apply(e,a),!(void 0!==r&&(t.exports=r))}});
//# sourceMappingURL=provider.html5.e86535d11f2f8dbcf3d5.map