
# coding: utf-8

# In[1]:

# import sqlite3
import MySQLdb
import mandrill


# In[3]:

#SQL 연결해서 데이터 가져오기
conn = MySQLdb.connect(host="localhost", user="root", passwd="asdf0234", db="congressReport")
cursor = conn.cursor()
cursor.execute('SELECT * from mailing_lst');
rows = cursor.fetchall()
rows = dict(rows)


# In[4]:

## 딕셔너리 형태 변환해서 리스트에 넣기

contents = []
to = 'to'

for k, v in rows.items():
    name = k
    email = v
    email_dict = {
        'name': name,
        'email': email,
        'type' : to
    }
    contents.append(email_dict)


# In[5]:

# 맨드릴로 이메일 보내
try:
    mandrill_client = mandrill.Mandrill('5ZtgEccM9JppbSujR9ntXA')
    message = {
#     'attachments': [{'content': 'ZXhhbXBsZSBmaWxl',
#                       'name': 'myfile.txt',
#                       'type': 'text/plain'}],
     'auto_html': None,
     'auto_text': None,
     'bcc_address': 'message.bcc_address@example.com',
     'from_email': 'joohee27@gmail.com',
     'from_name': 'JH_test3',
#      'global_merge_vars': [{'content': 'merge1 content', 'name': 'merge1'}],
#      'google_analytics_campaign': 'message.from_email@example.com',
#      'google_analytics_domains': ['example.com'],
     'headers': {'Reply-To': 'message.reply@example.com'},
     'html': '<p>Example HTML content</p>',
#      'images': [{'content': 'ZXhhbXBsZSBmaWxl',
#                  'name': 'IMAGECID',
#                  'type': 'image/png'}],
     'important': False,
     'inline_css': None,
     'merge': True,
     'merge_language': 'mailchimp',
#      'merge_vars': [{'rcpt': 'recipient.email@example.com',
#                      'vars': [{'content': 'merge2 content', 'name': 'merge2'}]}],
#      'metadata': {'website': 'www.example.com'},
     'preserve_recipients': None,
#      'recipient_metadata': [{'rcpt': 'recipient.email@example.com',
#                              'values': {'user_id': 123456}}],
     'return_path_domain': None,
     'signing_domain': None,
#      'subaccount': 'customer-123',
     'subject': 'example subject',
     'tags': ['password-resets'],
     'text': 'Example text content',
     'to': [contents],
     'track_clicks': None,
     'track_opens': None,
     'tracking_domain': None,
     'url_strip_qs': None,
     'view_content_link': None}
    result = mandrill_client.messages.send(message=message, async=False, ip_pool='Main Pool')
    '''
    [{'_id': 'abc123abc123abc123abc123abc123',
      'email': 'recipient.email@example.com',
      'reject_reason': 'hard-bounce',
      'status': 'sent'}]
    '''

except mandrill.Error, e:
    # Mandrill errors are thrown as exceptions
    print 'A mandrill error occurred: %s - %s' % (e.__class__, e)
    # A mandrill error occurred: <class 'mandrill.UnknownSubaccountError'> - No subaccount exists with the id 'customer-123'    
    raise

