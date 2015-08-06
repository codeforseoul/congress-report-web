$("#address").geocomplete({
  details: "form"
});


$("select#cities").bind('change', function(){
  getLocalList($('#cities').val());
});

$("select#locals").bind('change', function(){
  getTownList($('#cities').val(), $('#locals').val());
});

$("select#towns").bind('change', function(){
  getMemberIdx($('#cities').val(), $('#locals').val(), $('#towns').val());
});

function getLocalList (cityName) {
  $.get('/locals/' + cityName, function (locals) {
    var dList = [];
    var str = '';

    locals.result.forEach(function (d) {
      if (!(dList.indexOf(d.local) > -1)) {
        dList.push(d.local);
        str += '<option value="' + d.local + '">' + d.local + '</option>';
      }
    });

    $('#locals').html(str);
    $('#locals').prop('disabled', false);
  });
};

function getTownList (cityName, localName) {
  $.get('/towns/' + cityName + '/' + localName, function (towns) {
    var str = '';
    str += '<option value="" selected disabled>읍/면/동</option>';
    towns.result.forEach(function (t) {
      str += '<option value="' + t + '">' + t + '</option>';
    });

    $('#towns').html(str);
    $('#towns').prop('disabled', false);
  });
};

function getMemberIdx (cityName, localName, townName) {
  $.get('/member/' + cityName + '/' + localName + '/' + townName,
  function (idx) {
    alert(idx);
  });
};
