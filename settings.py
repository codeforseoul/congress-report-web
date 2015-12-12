# -*- coding: utf-8 -*-

import os

__all__ = ['MONGO_HOST', 'MONGO_PORT', 'MONGO_USERNAME', 'MONGO_PASSWORD', 'SERVER_IP', 'SERVER_PORT']

MONGO_HOST = 'localhost'
MONGO_PORT = 27017
MONGO_USERNAME = os.environ['mongoUsername']
MONGO_PASSWORD = os.environ['mongoPwd']
SERVER_IP = '0.0.0.0'
SERVER_PORT = 8080
