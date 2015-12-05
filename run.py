#-*- coding: utf-8 -*-
import json
import os

from districtfinder import find_cities, find_locals, find_towns, find_member_info
from flask import Flask, redirect, url_for, request, render_template
from flask.json import jsonify
from jinja2 import Environment, PackageLoader

from db import User
from settings import IP, PORT

district_data = json.load(open('election-data.json', 'r', encoding='utf-8'))
app = Flask(__name__)
env = Environment(loader=PackageLoader('index', 'templates'))

@app.route("/")
def index():
    template = env.get_template('index.html')
    return template.render(cities = district_data)

@app.route("/signup", methods=["POST"])
def signup():
    user = User(request.form['username'], request.form['email'])
    result = user.save()
    
    # TODO: return after saving user's data && error handling
    return redirect(url_for('index'))

@app.route("/locals/<city_name>", methods=["GET"])
def get_locals(city_name):
    return jsonify({"result": find_locals(city_name)})

@app.route("/towns/<city_name>/<local_name>", methods=["GET"])
def get_towns(city_name, local_name):
    return jsonify({"result": find_towns(city_name, local_name)})

@app.route("/member/<city_name>/<local_name>/<town_name>", methods=["GET"])
def get_member_info(city_name, local_name, town_name):
    return str(find_member_info(city_name, local_name, town_name))

if __name__ == "__main__":
    app.run(host=os.getenv('IP', IP), port=int(os.getenv('PORT', PORT)))
