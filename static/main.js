$("#address").geocomplete({
  details: "form"
});


$("select#cities").bind('change', function(){
  getLocalList($('#cities').val());

  var defaultOption = '<option value="" selected disabled>읍/면/동</option>';
  changeSelectState('#towns', defaultOption,true);
});

$("select#locals").bind('change', function(){
  getTownList($('#cities').val(), $('#locals').val());
});

$("select#towns").bind('change', function(){
  getMemberInfo($('#cities').val(), $('#locals').val(), $('#towns').val());
});

function getLocalList (cityName) {
  $.get('/locals/' + cityName, function (locals) {
    var dList = [];
    var str = '';

    str += '<option value="" selected disabled>시/군/구</option>';

    locals.result.forEach(function (d) {
      if (!(dList.indexOf(d.local) > -1)) {
        dList.push(d.local);
        str += '<option value="' + d.local + '">' + d.local + '</option>';
      }
    });

    changeSelectState('#locals', str, false);
  });
};

function getTownList (cityName, localName) {
  $.get('/towns/' + cityName + '/' + localName, function (towns) {
    var str = '';
    str += '<option value="" selected disabled>읍/면/동</option>';
    towns.result.forEach(function (t) {
      str += '<option value="' + t + '">' + t + '</option>';
    });

    changeSelectState('#towns', str, false);
  });
};

function getMemberInfo (cityName, localName, townName) {
  $.get('/member/' + cityName + '/' + localName + '/' + townName,
  function (info) {
    alert(info);
  });
};

function changeSelectState (selector, content, state) {
  $(selector).html(content);
  $(selector).prop('disabled', state);
};
