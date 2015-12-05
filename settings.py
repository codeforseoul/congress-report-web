# -*- coding: utf-8 -*-

import os

__all__ = ['MONGO_USERNAME', 'MONGO_PASSWORD', 'IP', 'PORT']

MONGO_USERNAME = os.environ['mongoUsername']
MONGO_PASSWORD = os.environ['mongoPwd']
IP = '0.0.0.0'
PORT = 8080
