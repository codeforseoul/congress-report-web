var oks = {
    FLASHBRIDGE: null,
    browserSupportsWebSockets: function () {
        return (
			window.WebSocket !== null && window.WebSocket !== undefined
		);
    },
    browserSupportsNativeWebSockets: (function () {
        return (
			window.WebSocket !== null && window.WebSocket !== undefined
		);
    })(),
    browserSupportsJSON: function () {
        return (
			window.JSON !== null && window.JSON !== undefined
		);
    },
    browserSupportsNativeJSON: (function () {
        return (
			window.JSON !== null && window.JSON !== undefined
		);
    })(),
    isIE: (function () {
        var lUserAgent = navigator.userAgent;
        var lIsIE = lUserAgent.indexOf("MSIE");
        return (lIsIE >= 0);
    })()
};
if (!oks.browserSupportsNativeJSON) {
    // <JasobNoObfs>
    // Please refer to http://json.org/js
    if (!this.JSON) { this.JSON = {}; } (function () { function f(n) { return n < 10 ? '0' + n : n; } if (typeof Date.prototype.toJSON !== 'function') { Date.prototype.toJSON = function (key) { return isFinite(this.valueOf()) ? this.getUTCFullYear() + '-' + f(this.getUTCMonth() + 1) + '-' + f(this.getUTCDate()) + 'T' + f(this.getUTCHours()) + ':' + f(this.getUTCMinutes()) + ':' + f(this.getUTCSeconds()) + 'Z' : null; }; String.prototype.toJSON = Number.prototype.toJSON = Boolean.prototype.toJSON = function (key) { return this.valueOf(); }; } var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' }, rep; function quote(string) { escapable.lastIndex = 0; return escapable.test(string) ? '"' + string.replace(escapable, function (a) { var c = meta[a]; return typeof c === 'string' ? c : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4); }) + '"' : '"' + string + '"'; } function str(key, holder) { var i, k, v, length, mind = gap, partial, value = holder[key]; if (value && typeof value === 'object' && typeof value.toJSON === 'function') { value = value.toJSON(key); } if (typeof rep === 'function') { value = rep.call(holder, key, value); } switch (typeof value) { case 'string': return quote(value); case 'number': return isFinite(value) ? String(value) : 'null'; case 'boolean': case 'null': return String(value); case 'object': if (!value) { return 'null'; } gap += indent; partial = []; if (Object.prototype.toString.apply(value) === '[object Array]') { length = value.length; for (i = 0; i < length; i += 1) { partial[i] = str(i, value) || 'null'; } v = partial.length === 0 ? '[]' : gap ? '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']' : '[' + partial.join(',') + ']'; gap = mind; return v; } if (rep && typeof rep === 'object') { length = rep.length; for (i = 0; i < length; i += 1) { k = rep[i]; if (typeof k === 'string') { v = str(k, value); if (v) { partial.push(quote(k) + (gap ? ': ' : ':') + v); } } } } else { for (k in value) { if (Object.hasOwnProperty.call(value, k)) { v = str(k, value); if (v) { partial.push(quote(k) + (gap ? ': ' : ':') + v); } } } } v = partial.length === 0 ? '{}' : gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}' : '{' + partial.join(',') + '}'; gap = mind; return v; } } if (typeof JSON.stringify !== 'function') { JSON.stringify = function (value, replacer, space) { var i; gap = ''; indent = ''; if (typeof space === 'number') { for (i = 0; i < space; i += 1) { indent += ' '; } } else if (typeof space === 'string') { indent = space; } rep = replacer; if (replacer && typeof replacer !== 'function' && (typeof replacer !== 'object' || typeof replacer.length !== 'number')) { throw new Error('JSON.stringify'); } return str('', { '': value }); }; } if (typeof JSON.parse !== 'function') { JSON.parse = function (text, reviver) { var j; function walk(holder, key) { var k, v, value = holder[key]; if (value && typeof value === 'object') { for (k in value) { if (Object.hasOwnProperty.call(value, k)) { v = walk(value, k); if (v !== undefined) { value[k] = v; } else { delete value[k]; } } } } return reviver.call(holder, key, value); } text = String(text); cx.lastIndex = 0; if (cx.test(text)) { text = text.replace(cx, function (a) { return '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4); }); } if (/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) { j = eval('(' + text + ')'); return typeof reviver === 'function' ? walk({ '': j }, '') : j; } throw new SyntaxError('JSON.parse'); }; } }());
    // </JasobNoObfs>
}

fetch_unix_timestamp = function () {
    //return parseInt(new Date().getTime().toString().substring(0, 10));
    return Math.floor(new Date().getTime() / 1000);
}
var timestamp = fetch_unix_timestamp();

function load_ajax(load, object, filter) {
    var filter = filter || null;
    var timestamp = fetch_unix_timestamp();
    $.post(load, { 'time': timestamp, 'filter': filter }, function (data) {
        $(object).html(data);
        if ($('aside#sub_aside').length > 0 && $('aside#sub_aside').length > 0 && ($('aside#sub_aside').height() + Number($('aside#sub_aside').css('padding-top').replace(/[^0-9]/g, ""))) > $('aside#sub_aside').height()) {
            $('aside#sub_aside').height($('div#sub_contents').height() - Number($('aside#sub_aside').css('padding-top').replace(/[^0-9]/g, "")));
        }
    });
}

$(document).ready(function () {
    if ($('aside#sub_aside').length > 0 && $('aside#sub_aside').length > 0 && ($('aside#sub_aside').height() + Number($('aside#sub_aside').css('padding-top').replace(/[^0-9]/g, ""))) < $('div#sub_contents').height()) {
        $('aside#sub_aside').height($('div#sub_contents').height() - Number($('aside#sub_aside').css('padding-top').replace(/[^0-9]/g, "")));
    }
});