dapr dashboard -p 9999




--------------------------
放在 C:\Users\Guisuwei\.dapr\components
subscription.yaml
apiVersion: dapr.io/v1alpha1
kind: Subscription
metadata:
  name: myevent-subscription
spec:
  topic: deathStarStatus
  route: /dsstatus
  pubsubname: pubsub
scopes:
- app1
- app2
运行发布订阅saidcar
dapr run --app-id testpubsub --dapr-http-port 3500