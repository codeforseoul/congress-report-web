$('#register').click(function () {
  $('.ui.small.modal')
    .modal({
      blurring: true
    })
    .modal('setting', 'transition', 'vertical flip')
    .modal('show');
});

['cities', 'locals', 'towns'].forEach(function (type) {
  switch(type) {
    case 'cities':
      $('#' + type).dropdown({
        onChange: function (value, text, $selectedItem) {
          getLocalList(value, function (locals) {
            $('#locals').dropdown('restore defaults');
            $('#towns').dropdown('restore defaults');

            var str = convertListToMenu(locals, 'local');

            changeSelectState('#locals', str, false);
            changeSelectState('#towns', '', false);
          });
        }
      });
      break;
    case 'locals':
      $('#' + type).dropdown({
        onChange: function (value) {
          getTownList($('#cities').dropdown('get value'), value, function (towns) {
            $('#towns').dropdown('restore defaults');

            var str = convertListToMenu(towns, 'town');

            changeSelectState('#towns', str, false);
          });
        }
      });
      break;
    case 'towns':
      $('#' + type).dropdown({
        onChange: function (value) {
          getMemberInfo($('#cities').dropdown('get value'), $('#locals').dropdown('get value'), value, function (member) {
            var imgProp = $('.modal .image-profile').prop;
            var defaultImg = 'http://semantic-ui.com/images/wireframe/white-image.png';

            member = JSON.parse(member.replace(/'/g, '"'));
            member.photo ? imgProp('src', member.photo) : imgProp('src', defaultImg);
          });
        }
      });
      break;
  }
});

function getLocalList (cityName, callback) {
  $.get('/locals/' + cityName, function (locals) {
    callback(locals);
  });
};

function getTownList (cityName, localName, callback) {
  $.get('/towns/' + cityName + '/' + localName, function (towns) {
    callback(towns);
  });
};

function getMemberInfo (cityName, localName, townName, callback) {
  $.get('/member/' + cityName + '/' + localName + '/' + townName,
  function (member) {
    callback(member);
  });
};

function changeSelectState (selector, content, state) {
  $(selector + ' .menu').html(content);
};

function convertListToMenu (list, type) {
  var localList = [];
  var str = '';

  switch (type) {
    case 'local':
      list.result.forEach(function (item) {
        if (localList.indexOf(item.local) === -1) {
          localList.push(item.local);
          str += '<div class="item" data-value="' + item.local + '">' + item.local + '</div>';
        }
      });
      break;
    case 'town':
      list.result.forEach(function (item) {
        if (localList.indexOf(item) === -1) {
          localList.push(item);
          str += '<div class="item" data-value="' + item + '">' + item + '</div>';
        }
      });
      break;
  }
  return str;
}
