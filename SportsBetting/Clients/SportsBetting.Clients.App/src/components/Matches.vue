<template>
  <div>
    <b-container class="container-row">
      <b-row>
        <b-col cols="1">Type</b-col>
        <b-col cols="4">Game</b-col>
        <b-col cols="3">1</b-col>
        <b-col cols="1">x</b-col>
        <b-col cols="3">2</b-col>
      </b-row>
    </b-container>
    <b-jumbotron >
      <b-container v-for="game in games">
        <router-link :to="{ path: `/matches/${game.Id}`}">
          <b-row class="game-row">
            <b-col cols="1">{{game.Type}}</b-col>
            <b-col cols="4">{{game.Tournament}}</b-col>
            <b-col cols="3">{{game.HomeTeam}}</b-col>
            <b-col cols="1">{{game.Score}}</b-col>
            <b-col cols="3">{{game.AwayTeam}}</b-col>
          </b-row>
        </router-link>
      </b-container>
    </b-jumbotron>
  </div>
</template>

<script>
import axios from 'axios'
import moment from 'moment'
import $ from 'jquery';

export default {
  name: 'Esports',
  data() {
    return {
      games: [],
      take: 20
    }
  },
  created() {
    this.games = [];
    this.take = 20;

    this.getEsports();
  },
  mounted() {
    let self = this;

    $(window).scroll(function() {
        if($(window).scrollTop() + $(window).height() > $(document).height() - 1) {
            self.getEsports();
        }
    });
  },
  methods: {
    getEsports() {
      axios.get(`http://localhost:64399/api/Matches/All/${this.take}`)
      .then(res => this.games = res.data, this.take += 20)
      .catch(err => console.log(err));
    }
  }
}
</script>

<style scoped>
  .container-row {
    font-weight: bold;
    font-size: 16px;
  }
  .jumbotron {
    background: #e01e5a;
    color: #fff;
    font-weight: bold;
  }
  .jumbotron a {
    text-decoration: none;
    color: #fff;
  }
  .row {
    margin-bottom: 20px;
    text-align: center;
  }
  .game-row:hover {
    background-color: #ff365f;
    border-radius: 10px;
  }
</style>
