var app = new Vue({
  el: '#app',
  data: {
    rows: [],
    countOfPage: 5,
    currPage: 1,
    filter_name: '',
    filteredRowCount: null
  },
  computed: {
    pageStart: function(){
        return (this.currPage - 1) * this.countOfPage;
      },
    totalPage: function(){
      if( this.filter_name.trim() === '' ) {
        return Math.ceil(this.rows.length / this.countOfPage);
      }
      else{
        return Math.ceil(this.filteredRowCount / this.countOfPage);
      }
    }
  },
  methods: {
    setPage: function(idx){
      if( idx <= 0 || idx > this.totalPage ){
        return;
      }
      this.currPage = idx;
    },
  },
  created: function(){
    var self = this;
    $.get('http://localhost:5000/Home/GetPersonal', function(data){
      self.rows = data;
    });
  }
});