var app = new Vue({
  el: '#app',
  data: {
    rows: [
    ],
    valueHeader:["00","01","02","03","04","05","06","07","08","09",
                 "10","11","12","13","14","15","16","17","18","19",
                 "20","21","22","23"],
    totalPage: 12,
    countOfPage: 5,
    countOfValue: 24,
    currPage: 1,
    filter_name: '',
    filteredRowCount: null
  },
  computed: {
    pageStart: function(){
        return (this.currPage - 1) * this.countOfPage;
      },
    IsActive: function(n){
      if(n == this.currPage)
      {
        return "disabled";
      }
      else
      {
        return "active";
      }
    }
  },
  methods: {
    setPage: function(idx){
      var apiUrl = 'http://localhost:5000/PMInfo/GetHistory?month=' + idx;
      axios.get(apiUrl).then((response) => {
        this.rows = response.data;
        this.currPage = idx;
      })
    },
  },
  created: function(){
    var self = this;
    axios.get('http://localhost:5000/PMInfo/GetHistory?month=1').then((response) => {
        self.rows = response.data;
    });
  }
});