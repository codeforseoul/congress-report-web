import logging

from pymongo import MongoClient
from settings import MONGO_HOST, MONGO_PORT, MONGO_USERNAME, MONGO_PASSWORD

client = MongoClient(MONGO_HOST, MONGO_PORT)
db = client.congress_report
users = db.users

class User:
    def __init__(self, username, email, assembly_id):
        self.username, self.email, self.assembly_id = username, email, assembly_id
    
    def save(self):
        response = { 'message': '', 'status': '' }

        try:
            response['status'] = 'success'
            response['message'] = users.insert_one({'username': self.username, 'email': self.email, 'assembly_id': self.assembly_id}).inserted_id
            return response

        except Exception as e:
            logging.exception(e)
            response['status'] = 'error'
            response['message'] = e
            return response
