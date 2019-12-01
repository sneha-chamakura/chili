$(function () {
  $('#downloadFormat').select2(Core.select2Options);
  $('#beginDate').datetimepicker(Core.dateTimePickerDateOptions);
  $('#endDate').datetimepicker(Core.dateTimePickerDateOptions);
});

const App = new Vue({
  el: '#app',
  data: function () {
    return {
      stations: [],
      model: {
        stationId: Core.DEFAULT_STATION
      }
    }
  },
  created: function() {
    Core.populateStationDropdown(this, false);
  },
  methods: {
    download: function () {
      // TODO
    }
  }
});
